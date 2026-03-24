# IMVA

IMVA is an interactive mixed reality anatomy learning project built with Unity and MRTK3 for immersive education and training scenarios.

![IMVA runtime screenshot in PC](./docs/images/simpleScreenshot.png)

# Background and Features

## Why this project

Traditional anatomy learning often lacks spatial depth and interaction. This project explores a mixed reality workflow where users can inspect, select, and manipulate anatomy content in 3D space with natural input.

## Core features

- Mixed reality anatomy exploration with MRTK3 interaction patterns.
- Selection, clipping, and cross-section related interaction workflows.
- Custom UI and editor tooling for scene and workflow iteration.
- Multi-platform deployment path (HoloLens / Android XR pipelines).

# Quick Start

This section is designed so you can open and run the project with minimal setup.

## Requirements

- Windows/macOS with Unity Hub installed
- **Unity 2021.3.45f2 LTS**
- Optional modules (recommended):
  - Universal Windows Platform Build Support (HoloLens 2)
  - Android Build Support (Quest / Android XR)

## Installation

1. Clone the repository:

```bash
git clone https://github.com/auroey/mr-IMVA-unity.git
```

2. Open **Unity Hub** -> **Add** -> select:

```text
UnityProjects/MRTKDevTemplate
```

3. Open the scene:

```text
Assets/Scenes/EmptyScene/my.unity
```

4. Press Play in Unity Editor.

# Usage

## Common workflows

- **Open project**: `UnityProjects/MRTKDevTemplate`
- **Main scene entry**: `Assets/Scenes/EmptyScene/my.unity`
- **Custom runtime logic**: `Assets/Scripts/`
- **Editor tooling**: `Assets/Editor/`

## Configuration

- Unity packages are managed in `UnityProjects/MRTKDevTemplate/Packages/manifest.json`.
- Project-wide settings live under `UnityProjects/MRTKDevTemplate/ProjectSettings/`.
- For reproducible environments, keep Unity version pinned to `2021.3.45f2`.

## Online dependencies

On first open, Unity will resolve external packages from Unity Registry and GitHub, so network access is required. Important dependencies include:

- `com.microsoft.mixedreality.openxr` (1.11.2)
- `com.microsoft.mixedreality.visualprofiler` (GitHub v2.2.0)
- `com.microsoft.mrtk.graphicstools.unity` (0.8.0)
- `com.microsoft.mrtk.tts.windows` (1.0.4)
- `com.microsoft.spatialaudio.spatializer.unity` (2.0.55)
- `com.unity.*` packages (URP, Input System, TextMeshPro, etc.)

# Architecture and Tech Stack

## Key repository structure

```text
.
├── UnityProjects/
│   └── MRTKDevTemplate/               # Unity project root
│       ├── Assets/
│       │   ├── Scripts/               # Runtime/game logic
│       │   ├── Editor/                # Editor tools
│       │   ├── Scenes/                # Scene assets
│       │   └── Models/                # Anatomy and other 3D assets
│       ├── Packages/manifest.json     # Unity package dependencies
│       └── ProjectSettings/           # Unity project settings
├── org.mixedrealitytoolkit.*          # MRTK3 local source packages
├── Pipelines/                         # CI/CD related configs
├── Tooling/                           # Utility scripts and tools
└── docs/                              # Documentation
```

## Tech stack

| Component | Version |
|---|---|
| Unity | 2021.3.45f2 LTS |
| Render Pipeline | URP 12.1.15 |
| XR Framework | OpenXR 1.13.0 + XR Interaction Toolkit 2.6.3 |
| MR Platform | Microsoft Mixed Reality OpenXR 1.11.2 |
| AR | AR Foundation + ARCore 5.0.5 |
| Hand Tracking | Unity XR Hands 1.3.0 |
| Input System | Unity Input System 1.7.0 |
| MRTK | Mixed Reality Toolkit 3 (v3.3.0 – v3.4.0 dev) |
| Graphics Tools | MRTK Graphics Tools 0.8.0 |
| Spatial Audio | Microsoft Spatial Audio 2.0.55 |
| 3D Import | glTFast 6.10.1 |
| Text | TextMeshPro 3.0.6 |
| JSON | Newtonsoft JSON + FullSerializer |
| Outline | UnityFx.Outline (Core + URP) |

## MRTK3 module dependency sketch

```text
core -> input -> spatialmanipulation -> uxcore -> uxcomponents
   |                                   \-> uxcomponents.noncanvas
   +-> audio
   +-> diagnostics
   +-> data (optional)
   +-> accessibility
   +-> windowsspeech
   \-> tools

standardassets -> extendedassets
```

# FAQ / Troubleshooting

## Package resolution failed on first open

- Confirm internet connectivity and proxy settings.
- Re-open Unity Hub and relaunch the project to retry package restore.
- Verify access to GitHub and Unity package registry endpoints.

## Scene cannot be opened or references are missing

- Ensure project root is `UnityProjects/MRTKDevTemplate` (not repository root).
- Confirm Unity version is exactly `2021.3.45f2`.
- Let Unity finish initial package import before opening scenes.

## Build target issues (HoloLens / Android XR)

- Install required Unity modules for the target platform in Unity Hub.
- Check XR/OpenXR package versions and target platform settings in Project Settings.

# Contributing / Development

Contributions are welcome. For smooth collaboration:

1. Fork and create a feature branch:

```bash
git checkout -b feature/your-feature-name
```

2. Keep changes focused and test in Unity Editor before opening a PR.
3. Write clear commit messages (recommended: Conventional Commits).
4. Open a Pull Request with:
   - what changed
   - why it changed
   - how it was tested

# License and Acknowledgements

- This project includes MRTK3-related components and is distributed under the [BSD 3-Clause License](./LICENSE.md).
- Thanks to the Unity and MRTK ecosystem maintainers and contributors.
