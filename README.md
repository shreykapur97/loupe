# Loupe

A second canvas for Grasshopper. Loupe adds a button to the Grasshopper
canvas toolbar (the strip with save, zoom, named views and preview modes).
Clicking it opens a small framed panel in the bottom-left of the canvas
with its own view of the same document, which you can zoom and pan
separately, so you can look at two parts of one script at once. Click
again to hide or show it.

Named after the jeweller's magnifier.

**Status:** early development; releases marked pre-release are untested.

## Requirements

- Rhino 8 (Windows), Grasshopper 1.

## Install

1. Download `Loupe.gha` from the
   [latest release](https://github.com/shreykapur97/loupe/releases/latest).
2. Right-click it → Properties → tick **Unblock** → OK (Windows blocks
   downloaded plugins).
3. In Grasshopper: `File → Special Folders → Components Folder`, and drop
   `Loupe.gha` in there.
4. Restart Rhino.

To uninstall, delete `Loupe.gha` from that folder and restart Rhino.

## Use

- Click the Loupe button on the canvas toolbar: the panel opens bottom-left.
- Zoom and pan in it as on the main canvas; selection and wires are shared.
- Lock (top-left): unlock to move the panel by its frame or resize it by a
  corner; lock again to keep it there.
- Home (top-left): back to the default size and place.
- Click the toolbar button again to hide it; click to bring it back.

The second canvas is meant for viewing and navigating. Some editing (wire
dragging, the search popup, widgets) may not work in it.

## Build

.NET SDK and Rhino 8 needed; see [`.docs/setup.md`](.docs/setup.md).

## Licence

[GPL-3.0](LICENSE). You can use, change and share Loupe; anything you
distribute that's based on it must be shared under the same licence, with
its source.
