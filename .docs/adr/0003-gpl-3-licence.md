# 0003 — GPL-3.0 licence

Date: 2026-10-27 · Status: accepted · Decided by: shrey

## Context

Shrey wants Loupe open source: people can edit it, but changes "need to be
shared with everyone". That's a copyleft licence. Options: GPL-3.0 (strong
copyleft), LGPL-3.0 or MPL-2.0 (weaker, file- or library-level).

## Decision

- Loupe is licensed GPL-3.0 (`LICENSE`).

## Consequences

- Anyone can use, change and redistribute Loupe; whatever they distribute
  that's based on it must be GPL-3.0 with its source.
- Loupe links to Rhino and Grasshopper, which are proprietary. Plugins for
  proprietary hosts are commonly GPL, but whether a host counts as a
  "system library" is a grey area. If that ever matters (someone wants to
  bundle Loupe), switch to MPL-2.0 or add a linking exception via a new ADR.
- Contributions come in under GPL-3.0.
