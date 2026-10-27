# 0001 — Rhino 8 only

Date: 2026-10-27 · Status: accepted · Decided by: shrey

## Context

Rhino 7 plugins target .NET Framework 4.8; Rhino 8 runs on modern .NET and
also loads net48. Supporting both means multi-targeting. Shrey: "lets just
do rhino 8 for now, since i use rhino 8".

## Decision

- Loupe targets Rhino 8 only, with the target framework the
  `dotnet new grasshopper` template sets for Rhino 8.
- No net48 / Rhino 7 build.

## Consequences

- One target framework: simpler project file and CI.
- Rhino 7 users can't use Loupe. Adding them later means a second target
  and a new ADR.
