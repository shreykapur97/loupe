# 0007 — Four buttons: pin, home, view only, match view

Date: 2026-10-27 · Status: accepted · Decided by: shrey
Updated 2026-10-27: lock and float merged into pin ("the lock button and
the popout can be merged").

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
  1. **Pin** (default on): pinned, Loupe sits on the canvas, fixed.
     Unpinned, it floats in its own borderless window (above the editor,
     anywhere on screen, not in the taskbar) that can be dragged by the
     frame and resized by a corner. Pinning again docks it where the
     window is if that overlaps the main canvas (pushed inside it),
     otherwise where it was last pinned.
  2. **Home:** pins Loupe, default size (480 × 360 at 100% scaling),
     bottom-left.
  3. **View only** (default on): no editing and no navigation — clicks,
     wheel, keys and drops are ignored. Off: a normal canvas.
  4. **Match view:** Loupe takes the main canvas's zoom and centre.
- **Opening:** the first click shows it docked, bottom-left, at the main
  canvas's current view (same zoom, same centre). Later clicks hide / show.
- **Closing the Grasshopper editor** hides Loupe, docked or floating.
- **One Loupe.** Nothing is remembered between sessions.
- No compass or Markov bar in Loupe.

## Consequences

- A frozen Loupe only moves by match view; to look around inside it, turn
  view only off.
- Pinned, Loupe can't leave the editor; unpinned, it can go anywhere,
  including another monitor. Rearranging it on the canvas means unpin,
  place, pin.
- A floating Loupe can't be locked in place.
- Rounded corners are slightly jagged (WinForms region clipping); the
  floating shadow may follow the window's rectangle, not its corners.
