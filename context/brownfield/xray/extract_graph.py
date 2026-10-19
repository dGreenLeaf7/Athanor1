#!/usr/bin/env python3
"""Build context/brownfield/xray/graph.json for the FLT codebase.

Re-runnable and auditable: `python3 -I context/brownfield/xray/extract_graph.py` from the repo root.

Why a script: the stack is VB.NET/C# on .NET Framework with old-style project files that
reference projects outside this repo (Contoso.Utilities, skmMenu), so no installed tool
(dotnet, madge, depcruise, jdeps) can produce a reliable graph here.

Leaves are source files (.vb .cs .aspx .ascx .asax .master .asmx .svc .ashx, own .js, .config)
plus datastore leaves for stored procedures and tables declared in .sql files.

Edges:
  call      a file mentions a type declared in another file (VB is case-insensitive, so matching
            is case-insensitive), or a public member of a VB Module (called without the module
            name). Restricted to the referencing project and the projects it references.
            Partial classes (code-behind + designer) link to each other both ways.
  dispatch  page directives (Inherits, CodeBehind, MasterPageFile, Register Src), URL strings that
            resolve to a page/handler (menus, Response.Redirect, NavigateUrl, Web.config, app.config
            HTTP targets), Web API controllers by convention, startup forms from Application.myapp,
            a project's entry points to its config file.
  read/write  a string literal that names a stored procedure (fetch/list/get/select/check/validate/
            search/count prefixes = read, otherwise write); proc -> table and proc -> proc from the SQL.
Over-linking is deliberate: a false edge can only make something look used, never dead.
"""
import json
import os
import re
import sys
from collections import defaultdict

ROOT = os.path.abspath(os.path.join(os.path.dirname(__file__), '..', '..', '..'))
OUT = os.path.join(ROOT, 'context', 'brownfield', 'xray', 'graph.json')

SKIP_DIRS = {'.git', '.vs', 'bin', 'obj', 'packages', 'node_modules', 'context', '.claude',
             'Backup', 'Crystal Reports Backup Files', 'aspnet_client'}
CODE_EXT = ('.vb', '.cs')
PAGE_EXT = ('.aspx', '.ascx', '.asax', '.master', '.asmx', '.svc', '.ashx')
URL_EXT = 'aspx|ashx|asmx|svc|master'
VENDORED_JS = re.compile(r'jquery|\.min\.js$|modernizr|bootstrap|respond|skm|_references', re.I)
READ_PREFIX = re.compile(r'^(fetch|list|get|select|check|validate|search|count|is|find|rpt|report)', re.I)

VB_DECL = re.compile(r'^\s*(?:<[^>]*>\s*)?(?:(?:Public|Friend|Private|Protected|Partial|MustInherit|NotInheritable|Shared|Shadows)\s+)*'
                     r'(Class|Module|Structure|Interface|Enum)\s+(\w+)', re.I)
VB_MODULE_MEMBER = re.compile(r'^\s*(?:(?:Public|Friend|Shared|Overloads)\s+)*(?:Sub|Function|Property|ReadOnly\s+Property|Const|Dim)\s+(\w+)', re.I)
VB_PRIVATE = re.compile(r'^\s*Private\b', re.I)
VB_END_MODULE = re.compile(r'^\s*End\s+Module\b', re.I)
CS_DECL = re.compile(r'\b(class|interface|enum|struct)\s+(\w+)')
IDENT = re.compile(r'[A-Za-z_]\w*')
STRLIT = re.compile(r'"([^"\r\n]*)"')
URL = re.compile(r'([~\w./\-]*?[\w\-]+\.(?:' + URL_EXT + r'))\b', re.I)
_BUILT_TAIL = r'"(\w+)"\s*(?:(&|\+)\s*[\w.()]+(?:\s*(?:&|\+)\s*"(\w*)")?)?'
CMDTEXT_LIT = re.compile(r'CommandText\s*=\s*' + _BUILT_TAIL, re.I)
SQLCMD_LIT = re.compile(r'New\s+SqlCommand\s*\(\s*' + _BUILT_TAIL, re.I)
IMPORTS = re.compile(r'^\s*(?:Imports|using)\s+([\w.]+)\s*;?\s*$', re.I)
CRYSTAL_LOAD = re.compile(r'\.Load\s*\(|ReportDocument', re.I)
SQL_CREATE = re.compile(r'\b(?:CREATE|ALTER)\s+(PROCEDURE|PROC|TABLE)\s+([\[\]\w.]+)', re.I)
SQL_EXEC = re.compile(r'\bEXEC(?:UTE)?\s+([\[\]\w.]+)', re.I)
SQL_WRITE = re.compile(r'\b(?:INSERT\s+(?:INTO\s+)?|UPDATE\s+|DELETE\s+(?:FROM\s+)?|MERGE\s+(?:INTO\s+)?)([\[\]\w.]+)', re.I)
SQL_READ = re.compile(r'\b(?:FROM|JOIN)\s+([\[\]\w.]+)', re.I)
COMMON_WORDS = {'new', 'string', 'integer', 'object', 'date', 'type', 'item', 'value', 'name', 'id', 'list',
                'me', 'my', 'end', 'sub', 'function', 'class', 'module', 'program'}


def rel(p):
    return os.path.relpath(p, ROOT).replace(os.sep, '/')


def read(p):
    with open(p, 'rb') as fh:
        return fh.read().decode('utf-8', errors='replace')


def strip_comments(text, ext):
    out = []
    for line in text.splitlines():
        if ext == '.vb':
            # drop a ' comment that is outside a string literal; drop #Region directives
            in_str, cut = False, None
            for i, ch in enumerate(line):
                if ch == '"':
                    in_str = not in_str
                elif ch == "'" and not in_str:
                    cut = i
                    break
            line = line if cut is None else line[:cut]
            if re.match(r'^\s*(REM\s|#(End\s+)?Region)', line, re.I):
                line = ''
        elif ext == '.cs':
            line = re.sub(r'//.*$', '', line)
            if re.match(r'^\s*#(end)?region', line, re.I):
                line = ''
        out.append(line)
    text = '\n'.join(out)
    if ext == '.cs':
        text = re.sub(r'/\*.*?\*/', lambda m: '\n' * m.group(0).count('\n'), text, flags=re.S)
    if ext in PAGE_EXT:
        text = re.sub(r'<%--.*?--%>', lambda m: '\n' * m.group(0).count('\n'), text, flags=re.S)
        text = re.sub(r'<!--.*?-->', lambda m: '\n' * m.group(0).count('\n'), text, flags=re.S)
    return text


# ---------------------------------------------------------------- projects
projects = {}  # dir -> {name, file, refs:set(dirs), included:set(rel paths)}
for dp, dns, fns in os.walk(ROOT):
    dns[:] = [d for d in dns if d not in SKIP_DIRS]
    for fn in fns:
        if fn.endswith(('.vbproj', '.csproj')):
            pfile = os.path.join(dp, fn)
            txt = read(pfile)
            included = set()
            for m in re.finditer(r'<(?:Compile|Content|None|EmbeddedResource)\s+Include="([^"]+)"', txt):
                included.add(os.path.normpath(os.path.join(dp, m.group(1).replace('\\', '/'))).lower())
            refs = set()
            for m in re.finditer(r'<ProjectReference\s+Include="([^"]+)"', txt):
                target = os.path.normpath(os.path.join(dp, m.group(1).replace('\\', '/')))
                refs.add(os.path.dirname(target))
            startup = re.search(r'<MainForm>(\w+)</MainForm>', read(os.path.join(dp, 'My Project', 'Application.myapp'))) \
                if os.path.exists(os.path.join(dp, 'My Project', 'Application.myapp')) else None
            startup_obj = re.search(r'<StartupObject>([\w.]+)</StartupObject>', txt)
            projects[dp] = {'name': os.path.splitext(fn)[0], 'file': rel(pfile), 'refs': refs,
                            'included': included, 'mainForm': startup.group(1) if startup else None,
                            'startupObject': startup_obj.group(1) if startup_obj else None}


def project_of(path):
    best = None
    for d in projects:
        if path == d or path.startswith(d + os.sep):
            if best is None or len(d) > len(best):
                best = d
    return best


def visible_projects(pdir):
    seen, stack = set(), [pdir]
    while stack:
        cur = stack.pop()
        if cur in seen or cur is None:
            continue
        seen.add(cur)
        stack.extend(r for r in projects.get(cur, {}).get('refs', ()) if r in projects)
    return seen


# ---------------------------------------------------------------- leaves
leaves = {}  # id -> leaf dict (with private _abs/_proj/_ext)
by_abs = {}


def leaf_id(r):
    return 'f:' + r


for dp, dns, fns in os.walk(ROOT):
    dns[:] = [d for d in dns if d not in SKIP_DIRS]
    for fn in fns:
        low = fn.lower()
        path = os.path.join(dp, fn)
        r = rel(path)
        ext = os.path.splitext(low)[1]
        if low.endswith('.designer.vb') or low.endswith('.designer.cs'):
            ext = '.vb' if low.endswith('.vb') else '.cs'
        keep = ext in CODE_EXT or ext in PAGE_EXT or low in ('web.config', 'app.config') or \
            (ext == '.js' and not VENDORED_JS.search(fn))
        if not keep:
            continue
        if re.search(r'(^|/)web\.(debug|release)\.config$', r, re.I):
            continue
        if fn.lower() in ('assemblyinfo.vb', 'assemblyinfo.cs') or 'My Project' in r.split('/'):
            # compiler-generated settings/resources: reachable whenever their project is
            pass
        pdir = project_of(path)
        text = read(path)
        kind = 'screen' if ext in ('.aspx', '.ascx', '.master') else 'module'
        lid = leaf_id(r)
        leaves[lid] = {'id': lid, 'name': fn, 'kind': kind,
                       'language': {'.vb': 'vbnet', '.cs': 'csharp', '.js': 'javascript'}.get(ext,
                                   'config' if ext == '.config' else 'aspnet'),
                       'loc': text.count('\n') + 1, 'file': r,
                       '_abs': path, '_proj': pdir, '_ext': ext, '_text': text}
        if pdir and ext in CODE_EXT + PAGE_EXT and path.lower() not in projects[pdir]['included']:
            leaves[lid]['inProjectFile'] = False
        by_abs[path.lower()] = lid

# ---------------------------------------------------------------- SQL datastores
procs, tables = {}, {}  # lower name -> {id, name, file, line}
sql_files = []
for dp, dns, fns in os.walk(ROOT):
    dns[:] = [d for d in dns if d not in SKIP_DIRS]
    for fn in fns:
        if fn.lower().endswith('.sql'):
            sql_files.append(os.path.join(dp, fn))
sql_bodies = defaultdict(list)  # proc lower name -> [(file, line, text)]
for sf in sql_files:
    lines = read(sf).splitlines()
    current = None
    for i, line in enumerate(lines, 1):
        m = SQL_CREATE.search(line)
        if m:
            name = re.sub(r'[\[\]]', '', m.group(2)).split('.')[-1]
            if m.group(1).upper().startswith('PROC'):
                key = name.lower()
                procs.setdefault(key, {'id': 'ds:proc:' + name, 'name': name, 'file': rel(sf), 'line': i})
                current = key
            else:
                tables.setdefault(name.lower(), {'id': 'ds:table:' + name, 'name': name, 'file': rel(sf), 'line': i})
                current = None
            continue
        if current:
            sql_bodies[current].append((rel(sf), i, line))

# ---------------------------------------------------------------- symbol table
symbols = defaultdict(set)  # lower name -> leaf ids declaring it
decl_site = {}
for lid, lf in leaves.items():
    ext = lf['_ext']
    if ext not in CODE_EXT:
        continue
    lf['_clean'] = strip_comments(lf['_text'], ext)
    in_module = False
    for i, line in enumerate(lf['_clean'].splitlines(), 1):
        if ext == '.vb':
            m = VB_DECL.match(line)
            if m:
                symbols[m.group(2).lower()].add(lid)
                decl_site.setdefault((m.group(2).lower(), lid), i)
                in_module = m.group(1).lower() == 'module'
                continue
            if in_module:
                if VB_END_MODULE.match(line):
                    in_module = False
                elif not VB_PRIVATE.match(line):
                    mm = VB_MODULE_MEMBER.match(line)
                    if mm and mm.group(1).lower() not in COMMON_WORDS:
                        symbols[mm.group(1).lower()].add(lid)
        else:
            for m in CS_DECL.finditer(line):
                symbols[m.group(2).lower()].add(lid)
for lid, lf in leaves.items():
    if '_clean' not in lf:
        lf['_clean'] = strip_comments(lf['_text'], lf['_ext'])

# ---------------------------------------------------------------- edges
edges, seen_edges = [], set()
dynamic_sites, dynamic_targets, dangling = [], [], []


def add_edge(src, dst, kind, site):
    if src == dst or (src, dst) in seen_edges:
        return
    seen_edges.add((src, dst))
    edges.append({'source': src, 'target': dst, 'kind': kind, 'site': site})


all_pages = defaultdict(list)  # basename lower -> [leaf ids]
for lid, lf in leaves.items():
    if lf['_ext'] in PAGE_EXT:
        all_pages[lf['name'].lower()].append(lid)


def resolve_url(url, src):
    """Resolve an .aspx-style URL string from leaf src to a page leaf."""
    lf = leaves[src]
    u = url.split('?')[0].replace('\\', '/')
    pdir = lf['_proj'] or os.path.dirname(lf['_abs'])
    candidates = []
    if u.startswith('~/') or u.startswith('/'):
        candidates.append(os.path.join(pdir, u.lstrip('~/')))
    else:
        candidates.append(os.path.join(os.path.dirname(lf['_abs']), u))
        candidates.append(os.path.join(pdir, u))
    if '://' in url:
        tail = re.sub(r'^[a-z]+://[^/]+/?', '', u, flags=re.I)
        candidates.append(os.path.join(pdir, tail))
    for c in candidates:
        hit = by_abs.get(os.path.normpath(c).lower())
        if hit:
            return [hit]
    base = os.path.basename(u).lower()
    hits = all_pages.get(base, [])
    same = [h for h in hits if leaves[h]['_proj'] == lf['_proj']]
    if same:
        return same
    # cross-project HTTP targets (desktop clients calling the web app) and folder-relative paths
    if hits and ('/' in u or '://' in url or lf['_ext'] in ('.config',) or lf['_proj'] not in
                 {leaves[h]['_proj'] for h in hits}):
        tail = '/'.join(u.lower().split('/')[-2:])
        exact = [h for h in hits if leaves[h]['file'].lower().endswith(tail)]
        return exact or hits
    return []


proc_lookup = procs
built_prefixes = []
for lid, lf in leaves.items():
    text, clean, ext = lf['_text'], lf['_clean'], lf['_ext']
    vis = visible_projects(lf['_proj']) if lf['_proj'] else {None}
    lines = clean.splitlines()
    # 1. type / module-member references
    for i, line in enumerate(lines, 1):
        if ext == '.config':
            break
        for tok in set(t.lower() for t in IDENT.findall(line)):
            for tgt in symbols.get(tok, ()):
                if tgt != lid and (leaves[tgt]['_proj'] in vis or lf['_proj'] is None):
                    add_edge(lid, tgt, 'call', f"{lf['file']}:{i}")
    # partial classes: same declared type in several files of the same project link both ways
    # (handled below via shared symbols)
    # 2. URLs to pages/handlers
    for i, line in enumerate(text.splitlines(), 1):
        for m in URL.finditer(line):
            for tgt in resolve_url(m.group(1), lid):
                add_edge(lid, tgt, 'dispatch', f"{lf['file']}:{i}")
        # page directives
        for m in re.finditer(r'\b(CodeBehind|CodeFile|Src)\s*=\s*"([^"]+)"', line, re.I):
            p = os.path.normpath(os.path.join(os.path.dirname(lf['_abs']), m.group(2).replace('~/', '')))
            tgt = by_abs.get(p.lower())
            if not tgt and m.group(2).startswith('~/') and lf['_proj']:
                tgt = by_abs.get(os.path.normpath(os.path.join(lf['_proj'], m.group(2)[2:])).lower())
            if tgt:
                add_edge(lid, tgt, 'dispatch', f"{lf['file']}:{i}")
        for m in re.finditer(r'\bInherits\s*=\s*"([\w.]+)"', line, re.I):
            for tgt in symbols.get(m.group(1).split('.')[-1].lower(), ()):
                if leaves[tgt]['_proj'] in vis:
                    add_edge(lid, tgt, 'dispatch', f"{lf['file']}:{i}")
        # 3. stored procedures named in string literals
        if ext in CODE_EXT + PAGE_EXT:
            for s in STRLIT.findall(line):
                key = s.strip().lower()
                if key in proc_lookup:
                    kind = 'read' if READ_PREFIX.match(key) else 'write'
                    add_edge(lid, proc_lookup[key]['id'], kind, f"{lf['file']}:{i}")
            for rx in (CMDTEXT_LIT, SQLCMD_LIT):
                for m in rx.finditer(line):
                    if m.group(2):
                        built_prefixes.append((m.group(1).lower(), (m.group(3) or '').lower(), f"{lf['file']}:{i}", lid))
        # 4. dynamic sites: Crystal reports loaded by file name
        if ext in CODE_EXT and re.search(r'ReportDocument\s*\.\s*Load\s*\(|_reportDocument\.Load\s*\(|\.rpt"', line, re.I):
            dynamic_sites.append({'site': f"{lf['file']}:{i}",
                                  'pattern': 'Crystal ReportDocument.Load(<name>.rpt) - .rpt files are not in the repo'})
        # 5. string-built page names (Response.Redirect(x & ".aspx"))
        if re.search(r'&\s*"\.aspx|\+\s*"\.aspx', line, re.I):
            dynamic_sites.append({'site': f"{lf['file']}:{i}", 'pattern': 'URL built from a variable + ".aspx"'})
        # 6. imports of namespaces that are not in the repo
        m = IMPORTS.match(line)
        if m and m.group(1).lower().startswith(('contoso.utilities', 'skmmenu')):
            dangling.append({'source': lid, 'ref': m.group(1), 'site': f"{lf['file']}:{i}"})

# string-built proc names: "fetch" & LookupName. The suffix comes from string literals in code
# (e.g. fltLookupNameList), so a proc is a possible target when prefix + some literal == its name.
# Bounded this way rather than "every proc starting with fetch", which would hide every dead fetch*.
literals = set()
for lf in leaves.values():
    if lf['_ext'] in CODE_EXT + PAGE_EXT:
        literals.update(s.strip().lower() for s in STRLIT.findall(lf['_clean']) if re.fullmatch(r'\s*\w+\s*', s))
for prefix, suffix, site, src in built_prefixes:
    dynamic_sites.append({'site': site, 'pattern': f'stored procedure name built as "{prefix}" & <lookup name> & "{suffix}"'})
    for key, p in procs.items():
        mid = key[len(prefix):len(key) - len(suffix)] if suffix else key[len(prefix):]
        if key.startswith(prefix) and key.endswith(suffix) and mid in literals:
            dynamic_targets.append({'id': p['id'], 'reason': f'"{prefix}" & "{mid}" & "{suffix}" (lookup name literal)',
                                    'site': site})
            add_edge(src, p['id'], 'read' if READ_PREFIX.match(key) else 'write', site)

# partial classes: designer <-> code-behind (both declare the same type)
for name, lids in symbols.items():
    lids = sorted(lids)
    if 1 < len(lids) <= 4:
        for a in lids:
            for b in lids:
                if a != b and leaves[a]['_proj'] == leaves[b]['_proj'] and \
                        os.path.dirname(leaves[a]['_abs']) == os.path.dirname(leaves[b]['_abs']):
                    add_edge(a, b, 'call', f"{leaves[a]['file']}:{decl_site.get((name, a), 1)}")

# SQL: proc -> proc (EXEC), proc -> table (read/write)
for key, body in sql_bodies.items():
    src = procs[key]['id']
    for f, i, line in body:
        for m in SQL_EXEC.finditer(line):
            t = re.sub(r'[\[\]]', '', m.group(1)).split('.')[-1].lower()
            if t in procs:
                add_edge(src, procs[t]['id'], 'call', f'{f}:{i}')
        for rx, kind in ((SQL_WRITE, 'write'), (SQL_READ, 'read')):
            for m in rx.finditer(line):
                t = re.sub(r'[\[\]]', '', m.group(1)).split('.')[-1].lower()
                if t in tables:
                    add_edge(src, tables[t]['id'], kind, f'{f}:{i}')

# ---------------------------------------------------------------- entry points
entry, entry_why = [], {}


def find(relpath):
    return by_abs.get(os.path.join(ROOT, relpath).lower())


def ep(lid, why):
    if lid and lid in leaves and lid not in entry:
        entry.append(lid)
        entry_why[lid] = why


for pdir, p in projects.items():
    in_proj = [l for l, lf in leaves.items() if lf['_proj'] == pdir]
    for l in in_proj:
        lf = leaves[l]
        n = lf['name'].lower()
        if n == 'global.asax':
            ep(l, 'ASP.NET application entry (IIS)')
        if lf['_ext'] == '.aspx' and os.path.dirname(lf['_abs']) == pdir and n in ('default.aspx', 'login.aspx'):
            ep(l, 'site root / forms loginUrl')
        if lf['_ext'] == '.svc' or lf['_ext'] == '.asmx':
            ep(l, 'service endpoint file')
        if re.search(r'/Controllers/\w+Controller\.cs$', lf['file']):
            ep(l, 'Web API / MVC controller (convention routing api/{controller})')
        if lf['_ext'] in CODE_EXT and re.search(r'^\s*(Public\s+)?(Shared\s+)?Sub\s+Main\b|static\s+\w+\s+Main\s*\(',
                                                 lf['_clean'], re.I | re.M):
            ep(l, 'process Main')
        if lf['_ext'] in CODE_EXT and re.search(r'\bServiceRoute\s*\(', lf['_clean']):
            ep(l, 'WCF ServiceRoute registration')
        if p['mainForm'] and lf['_ext'] in CODE_EXT and not lf['name'].lower().endswith('.designer.vb'):
            if re.search(r'\bClass\s+' + re.escape(p['mainForm']) + r'\b', lf['_clean'], re.I):
                ep(l, 'WinForms startup form (Application.myapp MainForm)')
        if lf['_ext'] == '.config':
            pass
    # the project's config is loaded whenever any of its entry points runs
    cfgs = [l for l in in_proj if leaves[l]['_ext'] == '.config']
    eps = [e for e in entry if leaves[e]['_proj'] == pdir]
    for c in cfgs:
        for e in eps:
            add_edge(e, c, 'dispatch', leaves[c]['file'] + ':1')
    # compiler-generated My Project / AssemblyInfo files are part of every build of the project
    # (and of a library project whenever any of its compiled files is used)
    generated = [l for l in in_proj if '/My Project/' in '/' + leaves[l]['file']
                 or leaves[l]['name'].lower().startswith('assemblyinfo')]
    users = eps or [l for l in in_proj if l not in generated and leaves[l]['_ext'] in CODE_EXT
                    and leaves[l].get('inProjectFile') is not False]
    for l in generated + (cfgs if not eps else []):
        for e in users:
            add_edge(e, l, 'dispatch', leaves[l]['file'] + ':1')

# WCF: service contract implementation is invoked by the ServiceRoute's type
for l, lf in leaves.items():
    m = re.search(r'ServiceRoute\s*\(\s*"[^"]*"\s*,[^,]*,\s*typeof\s*\(\s*(\w+)\s*\)', lf['_clean'])
    if m:
        for t in symbols.get(m.group(1).lower(), ()):
            add_edge(l, t, 'dispatch', lf['file'] + ':1')

# Web API: WebApiConfig/RouteConfig/FilterConfig/BundleConfig are called from Global.asax (via call edges already)

# ---------------------------------------------------------------- tree
domains = defaultdict(list)
for lid, lf in leaves.items():
    pdir = lf['_proj']
    dname = projects[pdir]['name'] if pdir else (lf['file'].split('/')[0] if '/' in lf['file'] else 'root')
    leaf = {k: v for k, v in lf.items() if not k.startswith('_')}
    domains[dname].append(leaf)
children = [{'id': 'dom:' + d, 'name': d, 'kind': 'domain', 'children': sorted(ls, key=lambda x: x['file'])}
            for d, ls in sorted(domains.items())]
ds_children = [{'id': p['id'], 'name': p['name'] + ' (proc)', 'kind': 'datastore'} for p in procs.values()] + \
              [{'id': t['id'], 'name': t['name'] + ' (table)', 'kind': 'datastore'} for t in tables.values()]
children.append({'id': 'dom:data', 'name': 'Data stores', 'kind': 'domain', 'children': ds_children})

# de-duplicate dynamic targets
dt, seen = [], set()
for d in dynamic_targets:
    if d['id'] not in seen:
        seen.add(d['id'])
        dt.append(d)

graph = {
    'system': 'Contoso Flight Log Tracking (FLT)',
    'method': 'context/brownfield/xray/extract_graph.py: regex parse of VB.NET/C#/ASPX (type and VB-module-member '
              'references scoped by ProjectReference, page directives, URL strings incl. config/menu/redirects, '
              'proc names in string literals) + SQL CREATE PROC/TABLE bodies; Backup trees excluded',
    'root': {'id': 'sys', 'name': 'FLT', 'kind': 'system', 'children': children},
    'edges': edges,
    'entryPoints': entry,
    'entryPointReasons': entry_why,
    'dynamicSites': dynamic_sites,
    'dynamicTargets': dt,
    'unboundedDynamic': [],
    'danglingRefs': dangling,
    'notInProjectFile': sorted(lf['file'] for lf in leaves.values() if lf.get('inProjectFile') is False),
    'observations': [],
}
with open(OUT, 'w') as fh:
    json.dump(graph, fh, indent=1)
print(f'leaves={len(leaves)} procs={len(procs)} tables={len(tables)} edges={len(edges)} '
      f'entry={len(entry)} dynamicSites={len(dynamic_sites)} dynamicTargets={len(dt)} dangling={len(dangling)} '
      f'notInProject={len(graph["notInProjectFile"])}', file=sys.stderr)
