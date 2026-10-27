# Setup

Tools and first run on a new machine.

Shape: install → first-time setup → daily commands → troubleshooting.

## 1. Install

| Tool | Why | Install | Check |
|---|---|---|---|
| Git | repo | `winget install Git.Git` | `git --version` |
| .NET SDK (8) | build | `winget install Microsoft.DotNet.SDK.8` | `dotnet --version` |
| VS Code + C# Dev Kit | editor, debugging | `winget install Microsoft.VisualStudioCode`, then the C# Dev Kit extension | — |
| Rhino 8 | run and test | rhino3d.com | — |

Visual Studio is not needed.

## 2. First-time setup

```
git clone https://github.com/shreykapur97/loupe.git
cd loupe
dotnet new install Rhino.Templates
```

Not scaffolded yet: phase 0 runs `dotnet new grasshopper` and moves the
result into `src/Loupe/` (`architecture.md` → Repo layout). The template
sets the Grasshopper NuGet reference, the Rhino 8 target framework and the
`.gha` output.

### Loading the plugin in Rhino

Once, in Rhino: run `GrasshopperDeveloperSettings`, add
`src/Loupe/bin/<config>/<framework>/`, restart Rhino. (Or copy the `.gha`
to the Libraries folder.)

## 3. Daily commands

Same table as CLAUDE.md → Commands.

| What | Where | Command |
|---|---|---|
| Build | repo root | `dotnet build` |
| Debug | VS Code | Run → start Rhino.exe, or attach to a running Rhino (`.vscode/launch.json`) |

Every rebuild needs a Rhino restart.

## 4. Troubleshooting

| Symptom | Fix |
|---|---|
| Rhino says the `.gha` is blocked | Right-click → Properties → Unblock (downloaded files only) |
| Build fails: file in use | Close Rhino; it locks the loaded `.gha` |
