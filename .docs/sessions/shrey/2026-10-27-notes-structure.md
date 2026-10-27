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

## Continued — release and first bug

1. **Install from Releases:** CI builds the `.gha` on every push; a
   manual run with a tag (or a pushed `v*` tag) publishes a GitHub
   Release. The session's git proxy blocks tag pushes, so `v0.1.0-alpha`
   was made by a manual workflow run.
2. **"the button doesn't show up":** decompiled `GH_DocumentEditor`: the
   toolbar is a `GH_Toolstrip` named `CanvasToolbar`. Likely cause: the
   loader waited for `CanvasCreated`, which may fire before plugins load.
   Fix: poll `RhinoApp.Idle` from `PriorityLoad`, find the strip by name
   first, and print a line on success.
3. **"can i run it as a command?":** a Rhino command needs a Rhino plugin
   (`.rhp`), not a `.gha`; a menu item with a shortcut in the Grasshopper
   editor is the cheap option.

## Continued — panel on the canvas, lock, home, icon

1. v0.1.1-alpha "looks good, works well". Changes asked: no X; lock and
   home instead; a frame like the image preview component; hide the
   compass and bar; a less generic icon.
2. Options discussed: panel inside the canvas vs floating window → inside,
   "it should always be on the canvas not just stay in place like the
   component" (fixed on screen, not panned with the canvas). Lock and home
   top-left → ADR 0005, supersedes 0002.
3. Widgets: each `GH_Canvas` builds its own widget list once, in its
   constructor, so clearing Loupe's removes compass and Markov bar there
   only.
4. Icon: four options drawn (L, grasshopper, jeweller's loupe, wire); D
   first, then B, "i like the idea that you are trying to look at a
   grasshopper" → ADR 0006.

## Continued — five buttons, floating, view only

1. v0.2.0-alpha "works well". Shrey asked for a toggle between the panel
   on the canvas and a floating window; stopped the build: "dont do it
   right now we are talking to discuss upgrades".
2. Decided in discussion → ADR 0007 (supersedes 0005): home docks back
   and resets the size; nothing remembered; closing Grasshopper hides
   Loupe; one Loupe; a view-only button, on by default, that "freezes
   naviation too"; Loupe opens at the main canvas's view and a match-view
   button resets it ("same zoom same center"); five buttons stacked on the
   left; the frame's "edges and the borders" like the panel component.
3. "take the icons from lucid" → Lucide (ADR 0008), rendered to PNG with
   Playwright from the npm package (the session can't reach unpkg or
   jsDelivr), licence shipped in the repo and the `.gha`.
4. "make sure the codebase architecture is clean": `LoupePanel` split
   into `LoupePanel` (frame, buttons, placement), `LoupeCanvas` (sync, view
   only, match view), `IconButton`, `FloatingHost`, `Style`, `Assets`.

## Worth rereading (view only)

- `GH_Canvas` subscribes its own handlers to its `MouseDown`, `KeyDown`…
  events (decompiled). `LoupeCanvas` overrides `OnMouseDown` etc. and
  doesn't call `base`, so those events never fire and Grasshopper never
  sees the input.
