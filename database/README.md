# FLT Training Database

All data in this folder is synthetic. Every name, aircraft, base, reading and comment was generated for training.

| File | What it is |
|---|---|
| `FLT_Schema.sql` | Full schema scripted from the legacy database: 62 tables, stored procedures, and constraints. Creates the `FLT` database. |
| `FLT_SampleData.sql` | Sample records. Run it after the schema script. |
| `FLT_Training.bak` | Schema plus sample data as a ready-to-restore backup. Needs SQL Server 2022 or later. |

## Setup

**Option A: restore the backup** (SQL Server 2022+)

```sql
RESTORE DATABASE [FLT] FROM DISK = N'<path>\FLT_Training.bak'
WITH MOVE 'FLT' TO N'<data dir>\FLT.mdf', MOVE 'FLT_log' TO N'<data dir>\FLT_log.ldf';
```

**Option B: run the scripts** (any recent SQL Server, including Docker)

```bash
sqlcmd -S localhost -U sa -P '<pwd>' -C -i FLT_Schema.sql
sqlcmd -S localhost -U sa -P '<pwd>' -C -i FLT_SampleData.sql
```

`FLT_Schema.sql` reports errors for six stored procedures. See "Known legacy defects" below. The rest of the script completes normally.

**SQL Server in Docker on macOS or Linux:**

```bash
docker run -d --platform linux/amd64 --name flt-sql -e ACCEPT_EULA=Y \
  -e 'MSSQL_SA_PASSWORD=<StrongPwd>' -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest
```

Then point the application connection strings (`Web.config` / `App.config`) at your server. They currently hold placeholder server names and `REDACTED` passwords.

## Sign-in accounts

Every account uses the password `Training#2026`.

| User | Access |
|---|---|
| `admin` | Super user: all programs, all modules, including Security and AppConfig |
| `pm.ca200` | Program manager for program 1 (CA-200 Function & Reliability) |
| `pm.ca200f` | Program manager for program 2 (CA-200F Cargo Variant) |
| `eng.sam` | Engineer role in both programs |
| `entry.casey` | Data Entry role, program 1 |
| `viewer.jo` | Read-only viewer, program 2 |
| `locked.user` | Locked-out account, for testing the lockout path |

## What's in it

- 2 programs with their own lookup lists: pilots, engineers, mechanics, models, engine types, liquids, and so on
- 5 test bases with 4 test profiles each, plus flight-test regions, weight definitions, and time zones
- 20 test aircraft with configuration, base assignment, status, and test assignments. A few are in the hangar or off test.
- About 1,100 flights from mid-June to September 2026, with Jet-A, oil, and hydraulic fills; per-region flight time; pilot comments; and engine ground runs
- Aircraft comments and 8 work orders with reasons, instructions, parts, and comments
- Roles, module permissions, and users

## Known legacy defects (intentionally preserved)

These problems are present in the legacy system itself. They're kept as discovery material for the modernization exercise.

- **Six stored procedures fail to create** because they reference columns that no longer exist: `existsDocument`, `listDocumentForWorkOrder`, `listDocuments`, `listWorkOrderForDocument`, `updateDocument`, and `fetchAircraftLocation`. The application code still calls `fetchAircraftLocation`.
- **Eight stored procedures reference tables that don't exist**: `DocType`, `AircraftTest`, and `RouteRegion`. They are `existsDocType`, `fetchDocType`, `listDocTypes`, `existsAircraftTest`, `listAircraftInfosForTest`, `listAircraftInfosNotForTest`, `listRouteForRegion`, and `listRegionForRoute`.
- **Report procedures use temp tables with narrow text columns.** For example, `rptFuelBurnByRefill` stops with a "String or binary data would be truncated" error when run against the sample data.
