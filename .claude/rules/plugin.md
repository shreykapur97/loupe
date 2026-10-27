---
paths:
  - "src/**"
  - "Loupe.sln"
  - ".vscode/**"
---

# Plugin rules (C# / Grasshopper)

## Layout

- `LoupeInfo.cs` — `GH_AssemblyInfo`: name, author, version, icon.
- `LoupeLoader.cs` — `GH_AssemblyPriority`; the editor doesn't exist at
  load, so it subscribes to the editor/canvas creation event and hands off
  there. Nothing else.
- `UI/ToolbarButton.cs` — finds the canvas toolbar (the `ToolStrip` with
  save, zoom, named views, preview modes) and inserts the button. The only
  file touching editor internals.
- `UI/LoupeWindow.cs` — the form hosting the second `GH_Canvas`; attaches,
  detaches and follows the active document.

## Code

- One target: `net7.0-windows` (ADR 0001).
- Grasshopper from the NuGet reference the template adds; no copied DLLs.
- No comments unless the code can't say it; no unused usings or members.

## Window (ADR 0002)

- One window per editor, created on the first click, docked bottom-left of
  the editor, owned by the editor window, resizable.
- Later clicks toggle hide / show. Closing with X hides; the window is only
  disposed when the editor closes.
- Its canvas has its own `Viewport`: zoom and pan are independent of the
  main canvas.

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
