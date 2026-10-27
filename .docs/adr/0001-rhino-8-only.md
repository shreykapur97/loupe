# 0001 — Rhino 8 only

Date: 2026-10-27 · Status: accepted · Decided by: shrey
Updated 2026-10-27: target framework set to `net7.0-windows` only.

## Context

Rhino 7 plugins target .NET Framework 4.8; Rhino 8 runs on modern .NET and
also loads net48. Supporting both means multi-targeting. Shrey: "lets just
do rhino 8 for now, since i use rhino 8".

## Decision

- Loupe targets Rhino 8 on Windows only: `net7.0-windows`.
- The template's default for Rhino 8 (`net7.0-windows;net7.0;net48`) is cut
  to that one target: no net48 (Rhino 7, or Rhino 8 started with
  `/netfx`), no plain net7.0 (Mac).

## Consequences

- One target framework: simpler project file and CI.
- Rhino 7 users, and Rhino 8 run in .NET Framework mode, can't use Loupe. Adding them later means a second target
  and a new ADR.
