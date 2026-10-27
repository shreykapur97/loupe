---
paths:
  - "src/**"
  - "Loupe.sln"
  - ".vscode/**"
---

# Plugin rules (C# / Grasshopper)

## Layout

- `LoupeInfo.cs` — `GH_AssemblyInfo`: name, author, version, icon.
- `LoupeLoader.cs` — `GH_AssemblyPriority`; waits on `RhinoApp.Idle`
  until the editor exists, then hands off. Nothing else.
- `Assets.cs` — loads embedded PNGs by name.
- `UI/ToolbarButton.cs` — finds the canvas toolbar (`CanvasToolbar`) and
  the main canvas, inserts the button, creates the panel next to the main
  canvas, hides it when the editor closes. The only file touching editor
  internals (`LoupePanel` only uses the main canvas's parent to dock back).
- `UI/LoupePanel.cs` — frame, buttons, move / resize, dock / float.
- `UI/LoupeCanvas.cs` — the second `GH_Canvas`: follows the document,
  redraws with the main canvas, view only, match view.
- `UI/IconButton.cs` — a square button showing one of two icons.
- `UI/FloatingHost.cs` — the borderless window used when floating.
- `UI/Style.cs` — colours and the rounded-rectangle path.
- `Resources/` — `loupe_24.png`, `Icons/*.png` (Lucide, ADR 0008); every
  PNG is embedded under its file name.

## Code

- One target: `net7.0-windows` (ADR 0001).
- Grasshopper from the NuGet reference the template adds; no copied DLLs.
- No comments unless the code can't say it; no unused usings or members.

## Panel (ADR 0007)

- One panel per editor, created on the first click; later clicks hide /
  show. No X.
- Buttons top-down: lock, home, float, view only, match view. Lock and
  view only start on.
- Docked: a sibling of the main canvas, over it, always inside its bounds.
  Floating: the same panel inside a `FloatingHost` owned by the editor.
- Sizes go through `S()` so they scale with the display.
- Icons come from `Resources/Icons` (Lucide); no glyphs drawn in code.

## View only

- `LoupeCanvas` overrides the input `On…` methods and skips `base` when
  view only is on; Grasshopper wires its handlers to those events, so they
  never run. Mouse enter / leave are blocked too, so a frozen Loupe never
  takes focus from the main canvas.
- Match view copies `Viewport.Zoom`, then `Viewport.MidPoint`, with
  `ComputeProjection()` after each.

## Sync

- Selection, wires and solutions live on the document, so they are shared.
- The Loupe canvas redraws when the main canvas paints
  (`CanvasPostPaintOverlay`), which covers solutions, selection and moves;
  a mouse-up in Loupe redraws the main canvas. Never redraw from Loupe's
  own paint events (loop).
- Follow the active document when the user switches files.
- Every event subscribed is unsubscribed on dispose; `Document = null`
  before the canvas goes.

## Testing

- Every rebuild needs a Rhino restart (Grasshopper loads plugins at startup).
- A layer is done when it's been checked in Rhino 8 on Shrey's machine; this
  can't be done from a cloud session.
