# Loupe — build phases

The build plan. Where Shrey is right now: `status/shrey.md`.

## Decided

Each confirmed item gets an ADR (`adr/_index.md`) and a one-line pointer here.

- **Target:** Rhino 8 only, as the template sets it. (ADR 0001)
- **Panel:** a framed panel pinned on the canvas or floating; four buttons
  down the left (pin, home, view only, match view); opens at the main
  canvas's view. (ADR 0007)
- **Button icons:** Lucide. (ADR 0008)
- **Icon:** a grasshopper under a magnifier. (ADR 0006)
- **Licence:** GPL-3.0. (ADR 0003)
- **Name:** Loupe, free on food4Rhino, GitHub and the Package Manager.
  (ADR 0004)

## To confirm

Choices from the handover (2026-10-27), before ADRs existed. When one is
confirmed, write its ADR and move it to Decided.

- **Approach:** the five steps in `architecture.md` (loader, toolbar,
  button, second `GH_Canvas` in an owned `Form`, sync and cleanup).
- **Tooling:** VS Code + .NET SDK + C# Dev Kit, scaffolded with
  `Rhino.Templates`; no Visual Studio.
- **Repo layout:** as in `architecture.md`.
- **CI:** a GitHub Actions build attaching the `.gha` to Releases (optional).

## Phases

Each phase is done when it works in Rhino 8 on Shrey's machine.

### 0. Foundation

- Scaffold with `dotnet new grasshopper`, restructure into the layout,
  `dotnet build` gives a `.gha` that loads. Written; loading not checked.
- `.vscode/launch.json` and `tasks.json` for build + debug. Written.

### 1. Button

- Loader waits for the editor; button with icon and tooltip on the canvas
  toolbar.

### 2. Window

- Second canvas on the active document, as a panel on the canvas or
  floating, four buttons (ADR 0007).

### 3. Sync and cleanup

- Refresh after solutions and object changes; follow document switches;
  detach on dispose.

### 4. Release

- CI build, GitHub Release, food4Rhino and/or Package Manager.

## Feature tracker

| Feature | Status |
|---|---|
| Plugin loads | works (v0.1.1-alpha) |
| Toolbar button | works (v0.1.1-alpha) |
| Second canvas window | works (v0.1.1-alpha) |
| Hide / show toggle | works (v0.2.0-alpha) |
| Panel on the canvas: frame, lock, home, no widgets | works (v0.2.0-alpha) |
| Panel-style frame, Lucide icons | works (v0.3.0-alpha) |
| Pin / float (lock and float merged) | written, not checked in Rhino |
| View only (default) | works (v0.3.0-alpha) |
| Match view, open at main view | works (v0.3.0-alpha) |
| Hides when the editor closes | works (v0.3.0-alpha) |
| Refresh on solve / changes | written, not checked in Rhino |
| Follows active document | written, not checked in Rhino |
| Cleanup on close | written, not checked in Rhino |

## Open

The one list of undecided things. When one is decided, write an ADR and
remove it here.

### Before phase 0

- **Git workflow:** commit to `main`, or short branches + PRs.

### Before phase 4

- **Distribution:** food4Rhino, Package Manager (Yak), GitHub Releases, or
  all three.
- **Mac:** whether to support Rhino 8 for Mac at all (the canvas is
  WinForms); Windows only until then.

### Later

- **More than one window:** whether a second Loupe ever makes sense.
- **Editing in the second canvas:** how much of wire dragging, search and
  widgets can be made to work there.
