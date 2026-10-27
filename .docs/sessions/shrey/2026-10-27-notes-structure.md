# 2026-10-27 — Notes structure for loupe

First session in loupe, a cloud session: from an empty repo and a handover
to a notes structure modelled on barhopper. Session
`session_01TZ8e1UagoGdMDXoEavmh7v`
(https://claude.ai/code/session_01TZ8e1UagoGdMDXoEavmh7v), branch
`claude/adoring-ride-dlo1hf`.

## How it went

1. **Repo reachable**, but empty: no commits, no branches.
2. **Handover** pasted by Shrey: goal, name, approach, quirks, tooling,
   repo layout, next steps. Now spread over `architecture.md` (approach,
   quirks, layout), `setup.md` (tooling), `phases.md` (next steps, To
   confirm) and `case-study.md` (name check).
3. **Name check** in the Package Manager failed: the session's network
   can't reach `yak.rhino3d.com`. Left for Shrey.
4. **Target:** "lets just do rhino 8 for now, since i use rhino 8" →
   ADR 0001.
5. **Repeat clicks:** "first click opens a default window bottom left that
   you can zoom in and out of, subsequent clicks just hide and unhide the
   window" → ADR 0002. Closing with X hides, so the toggle keeps working.
6. **Notes structure:** "use barhopper's structure". Kept: CLAUDE.md map,
   rules by area, `/wrap-up`, `.docs/` with phases, requirements,
   architecture, setup, case-study, ADRs, status and sessions. Dropped:
   the two-developer machinery (proposals, per-developer contesting),
   `design/`, `.claudeignore`. Rule files: `plugin.md`, `docs.md`.
7. **README and licence:** "a licence that is open source lets people edit
   but needs to be shared with everyone" → GPL-3.0, ADR 0003.

## Worth rereading

- **Copyleft:** GPL-3.0 means changed versions that are distributed must
  stay GPL-3.0 with source. Running or editing privately has no duty.
- **Docs are `.md`** here, following barhopper, though Shrey's default for
  documents is `.txt`.

## Left open

- Package Manager name check.
- Git workflow, distribution, Mac (`phases.md` → Open).
- Detach barhopper from this cloud session once done.

## Continued — name, scaffold and first code

1. **Barhopper detached:** the read-only copy deleted; the repo stays
   attached until the session ends.
2. **Push** first failed (403): reconnecting GitHub wasn't enough; adding
   `loupe` to the Claude GitHub App fixed it.
3. **Name:** "it's free yes" in the Package Manager → ADR 0004.
4. **"do it all and write the code":** scaffolded with
   `dotnet new grasshopper -p:v 8` (template installed from its `.nupkg`;
   the session can't reach NuGet search), cut to `net7.0-windows` only
   (ADR 0001 updated), layout as in `architecture.md`, sample component
   dropped. Wrote the loader, toolbar button, window, VS Code configs and
   CI. Built against the real Grasshopper package with a net7.0 copy; the
   Linux SDK can't build `-windows` targets.

## Worth rereading (code)

- **Why `RhinoApp.Idle`:** `CanvasCreated` fires while the editor is still
  being built, so `Instances.DocumentEditor` can be null; Idle waits until
  it exists. The handler unsubscribes first, so Loupe's own canvas doesn't
  trigger it again.
- **Sync without a loop:** main paints → Loupe redraws; Loupe mouse-up →
  main redraws. Loupe's paint never triggers main.
