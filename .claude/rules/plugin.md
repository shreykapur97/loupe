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
- `UI/ToolbarButton.cs` — finds the canvas toolbar (`CanvasToolbar`) and
  the main canvas, inserts the button, and puts the panel next to the main
  canvas. The only file touching editor internals.
- `UI/LoupePanel.cs` — the framed panel hosting the second `GH_Canvas`,
  with lock and home; attaches, detaches and follows the active document.

## Code

- One target: `net7.0-windows` (ADR 0001).
- Grasshopper from the NuGet reference the template adds; no copied DLLs.
- No comments unless the code can't say it; no unused usings or members.

## Panel (ADR 0005)

- One panel per editor, created on the first click, a sibling of the main
  canvas, over it; later clicks hide / show. No X.
- Frame and buttons drawn by Loupe; sizes go through `S()` so they scale
  with the display.
- Lock (default on) blocks move and resize; home restores 480 × 360,
  bottom-left. It always stays inside the main canvas's bounds.
- Its canvas has its own `Viewport` and an empty widget list.

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
