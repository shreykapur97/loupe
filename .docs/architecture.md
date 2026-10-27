# Architecture

How Loupe hooks into Grasshopper. Plan and status: `phases.md` /
`status/shrey.md`.

## System

```
 Rhino 8 starts Grasshopper
   └─ loads Loupe.gha
        └─ LoupeLoader (GH_AssemblyPriority)
             └─ RhinoApp.Idle until Instances.DocumentEditor exists
                  └─ ToolbarButton: find the canvas ToolStrip, add button
                       └─ click ─▶ LoupeWindow (Form, owned by the editor)
                                     └─ GH_Canvas, Document = active document
                                        own Viewport (zoom / pan)

 GH_Document (shared): objects, wires, selection, solutions
   ├─ main canvas (Instances.ActiveCanvas)
   └─ Loupe canvas
```

1. **Entry point:** `GH_AssemblyPriority` runs at plugin load. The editor
   doesn't exist yet, so it subscribes to the editor or canvas creation
   event and does its setup there.
2. **Toolbar:** `Instances.DocumentEditor`, searched for the `ToolStrip`
   named `CanvasToolbar` (a `GH_Toolstrip` above the canvas in the
   editor's `Panel1`, found by decompiling Grasshopper 8); if that name
   changes, the lowest `ToolStrip` above the main canvas. Hidden when
   View → Canvas Toolbar is off.
3. **Button:** a `ToolStripButton` with icon and tooltip on that strip.
4. **Second canvas:** a new `GH_Canvas`, `Document` = active document, in a
   resizable `Form` owned by the editor. Each canvas has its own `Viewport`.
5. **Sync:** the document is shared; the Loupe canvas redraws whenever
   the main canvas paints, and follows `DocumentChanged` on the main canvas.
6. **Clicks:** first opens, then hide / show (ADR 0002).
7. **Cleanup:** on dispose, `Document = null` and unsubscribe everything.

## Known quirks

- Some Grasshopper interaction code assumes `Instances.ActiveCanvas`, the
  main canvas. Wire dragging, the search popup and widgets may be
  unreliable in the Loupe canvas.
- A script component would re-add the button on every solve, so it must be
  a compiled plugin.
- Grasshopper loads plugins only at startup: every rebuild needs a Rhino
  restart.

## Repo layout

```
loupe/
├── CLAUDE.md, README.md, LICENSE, .gitignore
├── Loupe.sln
├── .claude/              rules/, skills/, settings.json
├── .docs/                phases, requirements, architecture, setup,
│                         case-study, status/, sessions/, adr/
├── .vscode/              launch.json, tasks.json
├── .github/workflows/    build.yml (optional CI)
├── docs/images/          screenshots for the README
└── src/Loupe/
    ├── Loupe.csproj
    ├── LoupeInfo.cs      GH_AssemblyInfo
    ├── LoupeLoader.cs    GH_AssemblyPriority entry point
    ├── UI/
    │   ├── ToolbarButton.cs   the only file touching editor internals
    │   └── LoupeWindow.cs     form + second GH_Canvas
    └── Resources/loupe_24.png
```

`docs/images/` comes with the first screenshots.

## Environments

| | Where | How |
|---|---|---|
| Dev | Shrey's Windows machine, Rhino 8 | `dotnet build`, `GrasshopperDeveloperSettings` → `bin` folder |
| CI | GitHub Actions (optional) | builds the `.gha`, attaches it to Releases |
| Users | Grasshopper Libraries folder | copy the `.gha`, restart Rhino |
