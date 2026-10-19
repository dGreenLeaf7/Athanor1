#!/usr/bin/env bash
# Workshop setup. The Codespace runs this once when it is created (postCreateCommand).
# You can re-run it by hand at any time:  bash scripts/workshop-setup.sh
set -u

# Pinned to the version the codebase-xray skill calls, so the first diagram render needs no download.
MERMAID_CLI="@mermaid-js/mermaid-cli@11.16.0"

step() { printf '\n== %s\n' "$1"; }

step "Claude Code"
npm install -g @anthropic-ai/claude-code

step "codebase-xray plugin"
# .claude/settings.json also declares it; installing now means its skills load on the first 'claude' start.
if claude plugin marketplace add code-exitos/codebase-xray && claude plugin install codebase-xray@codebase-xray; then
  echo "Plugin installed."
else
  echo "Plugin not pre-installed. Claude Code will offer it from .claude/settings.json when you trust this folder."
fi

step "System libraries for headless Chrome (used to render diagrams to SVG)"
sudo apt-get update -qq
sudo DEBIAN_FRONTEND=noninteractive apt-get install -y -qq --no-install-recommends \
  libnss3 libatk1.0-0 libatk-bridge2.0-0 libcups2 libdrm2 libxkbcommon0 libxcomposite1 \
  libxdamage1 libxfixes3 libxrandr2 libgbm1 libasound2 libpango-1.0-0 libcairo2 \
  fonts-liberation >/dev/null

step "Diagram renderer ($MERMAID_CLI) and a test render"
tmp=$(mktemp -d)
printf 'flowchart LR\n  A[ready] --> B[render]\n' > "$tmp/t.mmd"
if npx -y -p "$MERMAID_CLI" mmdc -i "$tmp/t.mmd" -o "$tmp/t.svg" >/dev/null 2>&1 && [ -s "$tmp/t.svg" ]; then
  echo "Diagram test render works."
else
  echo "Diagram test render failed. Run: bash scripts/workshop-check.sh"
fi
rm -rf "$tmp"

echo
echo "Setup finished. Run: bash scripts/workshop-check.sh"
