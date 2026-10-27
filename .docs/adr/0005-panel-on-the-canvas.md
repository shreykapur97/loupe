# 0005 — Loupe is a framed panel on the canvas, with lock and home

Date: 2026-10-27 · Status: accepted · Decided by: shrey

## Context

The first version (ADR 0002) was a separate tool window with a title bar
and an X. Shrey, after trying it: no X; "a small lock … that locks the
window in place or lets you make it get large or small or move it
elsewhere when unlocked, and a home button that makes the window the
default size and default location"; the border should look like
Grasshopper's own frames, like the image preview component; and "it
should always be on the canvas not just stay in place like the
component".

## Decision

- Loupe is a panel placed over the main canvas, inside the editor, fixed
  on screen: it doesn't pan or zoom with the main canvas. It can't leave
  the editor.
- A rounded grey frame, drawn by Loupe, like Grasshopper's image
  preview; no title bar, no X.
- Top-left, over its canvas: **lock** and **home**.
  - Locked (default): can't be moved or resized.
  - Unlocked: drag the frame to move, drag a corner to resize.
  - Home: default size (480 × 360 at 100% scaling), bottom-left of the
    main canvas.
- The toolbar button still creates it on first click, then hides and
  shows it. That's the only way to hide it.
- It stays inside the main canvas: anchored bottom-left when the editor
  resizes, pushed back in if the editor gets smaller.
- No compass or Markov bar inside Loupe (its canvas's widget list is
  emptied).

## Consequences

- Looks like part of Grasshopper; nothing extra in the taskbar or on
  other monitors.
- Rounded corners are slightly jagged (WinForms region clipping).
- Supersedes ADR 0002 (separate window that opens once, then toggles;
  X hides it).
