# 0007 — Five buttons: lock, home, float, view only, match view

Date: 2026-10-27 · Status: accepted · Decided by: shrey

## Supersedes

ADR 0005: Loupe as a framed panel on the canvas with lock and home, a grey
gradient frame, always inside the editor.

## Context

After trying v0.2.0-alpha, Shrey asked for: a way to switch between the
panel on the canvas and "a floating window that can be placed anywhere";
a mode that "freezes the ability to edit the canvas, it's like a view only
window"; opening "to exactly what the canvas sees", with a button that
resets Loupe to the main canvas's current view; and the frame's "edges and
the borders" as clean as Grasshopper's panel component.

## Decision

- **Frame:** thick near-black rounded outline and a thin light rim, like
  the panel component. Same frame docked and floating; floating adds a
  drop shadow. No title bar, no X.
- **Buttons**, stacked down the left edge, over the canvas:
  1. **Lock** (default on): no move or resize. Unlocked: drag the frame to
     move, a corner to resize.
  2. **Home:** docks Loupe back on the canvas, default size (480 × 360 at
     100% scaling), bottom-left.
  3. **Float / dock:** moves Loupe between the main canvas and its own
     borderless window (above the editor, anywhere on screen, not in the
     taskbar). Docking back keeps its place, pushed inside the canvas.
  4. **View only** (default on): no editing and no navigation — clicks,
     wheel, keys and drops are ignored. Off: a normal canvas.
  5. **Match view:** Loupe takes the main canvas's zoom and centre.
- **Opening:** the first click shows it docked, bottom-left, at the main
  canvas's current view (same zoom, same centre). Later clicks hide / show.
- **Closing the Grasshopper editor** hides Loupe, docked or floating.
- **One Loupe.** Nothing is remembered between sessions.
- No compass or Markov bar in Loupe.

## Consequences

- A frozen Loupe only moves by match view; to look around inside it, turn
  view only off.
- Docked, Loupe can't leave the editor; floating, it can go anywhere,
  including another monitor.
- Rounded corners are slightly jagged (WinForms region clipping); the
  floating shadow may follow the window's rectangle, not its corners.
