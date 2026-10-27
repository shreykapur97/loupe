# Loupe — build phases

The build plan. Where Shrey is right now: `status/shrey.md`.

## Decided

Each confirmed item gets an ADR (`adr/_index.md`) and a one-line pointer here.

- **Target:** Rhino 8 only, as the template sets it. (ADR 0001)
- **Window:** first click opens it bottom-left; later clicks hide / show;
  closing hides. (ADR 0002)
- **Licence:** GPL-3.0. (ADR 0003)

## To confirm

Choices from the handover (2026-10-27), before ADRs existed. When one is
confirmed, write its ADR and move it to Decided.

- **Name:** Loupe. Free on food4Rhino and GitHub; not yet checked in the
  Rhino Package Manager (`case-study.md` → Findings).
- **Approach:** the five steps in `architecture.md` (loader, toolbar,
  button, second `GH_Canvas` in an owned `Form`, sync and cleanup).
- **Tooling:** VS Code + .NET SDK + C# Dev Kit, scaffolded with
  `Rhino.Templates`; no Visual Studio.
- **Repo layout:** as in `architecture.md`.
- **CI:** a GitHub Actions build attaching the `.gha` to Releases (optional).

## Phases

Each phase is done when it works in Rhino 8 on Shrey's machine.

### 0. Foundation

- Name checked in the Package Manager.
- Scaffold with `dotnet new grasshopper`, restructure into the layout,
  `dotnet build` gives a `.gha` that loads.
- `.vscode/launch.json` and `tasks.json` for build + debug.

### 1. Button

- Loader waits for the editor; button with icon and tooltip on the canvas
  toolbar.

### 2. Window

- Second canvas on the active document, bottom-left, resizable, toggle
  (ADR 0002).

### 3. Sync and cleanup

- Refresh after solutions and object changes; follow document switches;
  detach on dispose.

### 4. Release

- CI build, GitHub Release, food4Rhino and/or Package Manager.

## Feature tracker

| Feature | Status |
|---|---|
| Plugin loads | not started |
| Toolbar button | not started |
| Second canvas window | not started |
| Hide / show toggle | not started |
| Refresh on solve / changes | not started |
| Follows active document | not started |
| Cleanup on close | not started |

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
