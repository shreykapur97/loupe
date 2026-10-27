# 0008 — Panel button icons from Lucide

Date: 2026-10-27 · Status: accepted · Decided by: shrey

## Context

The first lock and home glyphs were drawn in code. Shrey: "take the icons
from lucid i think that is better". Lucide is an open icon set (ISC
licence, compatible with GPL-3.0).

## Decision

- The panel buttons use Lucide icons (lucide-static 1.62.0): `lock` /
  `lock-open`, `house`, `square-arrow-out-up-right` /
  `square-arrow-down-left`, `eye` / `pencil`, `scan`.
- Rendered once to 48 px PNGs in `#282828` by `tools/icons/render.js`,
  embedded in the `.gha`, scaled down when drawn.
- Lucide's licence ships in the repo and inside the `.gha`
  (`Resources/Icons/LICENSE-lucide.txt`).
- The plugin's own icon stays the grasshopper under a magnifier
  (ADR 0006).

## Consequences

- A new icon means rerunning `render.js` (Node + Playwright, `setup.md` →
  Icons), not drawing code.
- Icons swap to show state (lock / lock-open, eye / pencil, out / in).
