---
paths:
  - ".docs/**"
---

# Docs rules

## ADRs (`.docs/adr/`)

- Written when a decision is made, not upfront. Choices from before the
  folder existed sit in `phases.md` → To confirm; confirming one writes its
  ADR and moves it to Decided.
- ADRs are for decisions: target, architecture, tools, behaviour, and how
  the repo and its notes are structured. Habits go in `CLAUDE.md` or
  `.claude/rules/`, not an ADR.
- One file per decision, numbered in order: `NNNN-short-title.md`, header
  `Date: YYYY-MM-DD · Status: accepted · Decided by: shrey`, sections
  Context → Decision → Consequences. Grouping is the Area column in
  `adr/_index.md`.
- One developer, so an ADR is edited in place when it changes (an
  `Updated YYYY-MM-DD: …` line under the header); a different decision gets
  its own ADR and the old one becomes `superseded by NNNN`.
- Status is `accepted` or `superseded by NNNN`, nothing else.
- Open questions live in `phases.md` → Open; write the ADR once decided,
  then remove the item from Open.

## Open decisions and research

- `phases.md` → Open holds only the undecided item, short, with a pointer.
- Research (plugins looked at, API findings, name checks) goes in
  `case-study.md` → Findings.

## General

- `status/shrey.md` and `sessions/shrey/YYYY-MM-DD-<topic>.md` are written
  by `/wrap-up`.
- One fact in one place: link to the file that owns it, don't copy it.
