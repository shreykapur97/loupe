# 0002 — The window opens once, bottom-left, then hides and shows

Date: 2026-10-27 · Status: accepted · Decided by: shrey

## Context

Repeat clicks on the toolbar button could open more windows or focus the
existing one. Shrey: "first click opens a default window bottom left that
you can zoom in and out of, subsequent clicks just hide and unhide the
window".

## Decision

- The first click creates one Loupe window, bottom-left of the editor,
  owned by it, resizable, with its own zoom and pan.
- Every later click toggles hide / show.
- Closing it with X hides it, so the button still brings it back.
- It's disposed only when the editor closes.

## Consequences

- One Loupe window at a time; no list of windows to manage.
- The window keeps its size, place and view between hide and show.
- A hidden window must not redraw (`requirements.md` → Performance).
- More than one window stays open for later (`phases.md` → Open).
