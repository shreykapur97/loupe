# Loupe

A second canvas for Grasshopper. Loupe adds a button to the Grasshopper
canvas toolbar (the strip with save, zoom, named views and preview modes).
Clicking it opens a small, resizable window in the bottom-left with its own
canvas on the same document, which you can zoom and pan separately, so you
can look at two parts of one script at once. Click again to hide or show it.

Named after the jeweller's magnifier.

**Status:** early development, nothing to install yet.

## Requirements

- Rhino 8 (Windows), Grasshopper 1.

## Install

Not released yet. Once it is: download `Loupe.gha` from Releases, put it in
the Grasshopper Libraries folder (`File → Special Folders → Components
Folder`), unblock it if Windows asks, and restart Rhino.

## Use

- Click the Loupe button on the canvas toolbar: the window opens bottom-left.
- Zoom and pan in it as on the main canvas; selection and wires are shared.
- Click the button again (or close the window) to hide it; click to bring it
  back.

The second canvas is meant for viewing and navigating. Some editing (wire
dragging, the search popup, widgets) may not work in it.

## Build

.NET SDK and Rhino 8 needed; see [`.docs/setup.md`](.docs/setup.md).

## Licence

[GPL-3.0](LICENSE). You can use, change and share Loupe; anything you
distribute that's based on it must be shared under the same licence, with
its source.
