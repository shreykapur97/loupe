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

Click the Loupe button on the canvas toolbar: Loupe opens bottom-left,
showing exactly what the main canvas shows. Move around the main canvas;
Loupe keeps its view. Click the toolbar button again to hide or show it.

The buttons down Loupe's left edge, top to bottom:

| Button | What it does |
|---|---|
| Lock | On: Loupe stays put. Off: drag the frame to move it, a corner to resize it. |
| Home | Back on the canvas, default size, bottom-left. |
| Float / dock | Pops Loupe out into its own window you can put anywhere, and back. |
| View only | On: Loupe is frozen, no editing, no pan or zoom. Off: a normal canvas. |
| Match view | Loupe jumps to the main canvas's current view. |

Closing the Grasshopper editor hides Loupe.

The second canvas is meant for viewing and navigating. Some editing (wire
dragging, the search popup, widgets) may not work in it.

## Build

.NET SDK and Rhino 8 needed; see [`.docs/setup.md`](.docs/setup.md).

## Licence

[GPL-3.0](LICENSE). You can use, change and share Loupe; anything you
distribute that's based on it must be shared under the same licence, with
its source.

Button icons: [Lucide](https://lucide.dev), ISC licence
([`src/Loupe/Resources/Icons/LICENSE-lucide.txt`](src/Loupe/Resources/Icons/LICENSE-lucide.txt)).
