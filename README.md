# Mixed Reality Anatomy Project

A mixed reality anatomy learning and interaction application built with **Unity 2021.3.45f2 LTS** and **MRTK3** (Mixed Reality Toolkit 3).

This repository contains the full MRTK3 source alongside the Unity project, so all local package references resolve out of the box — no extra downloads needed.

## Tech Stack

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

## Getting Started

### Prerequisites

- **Unity 2021.3.45f2** (install via [Unity Hub](https://unity.com/download))
- **Universal Windows Platform Build Support** module (if deploying to HoloLens 2)
- **Android Build Support** module (if deploying to Quest / Android XR)

### Setup

1. Clone this repository:

```bash
git clone https://github.com/auroey/mr-anatomy-education-mrtk3-cpp.git
```

2. Open **Unity Hub** → **Add** → select `UnityProjects/MRTKDevTemplate` as the project folder.

### Online Dependencies

The following packages are fetched from the Unity registry / GitHub on first open — a network connection is required:

- `com.microsoft.mixedreality.openxr` (1.11.2)
- `com.microsoft.mixedreality.visualprofiler` (GitHub v2.2.0)
- `com.microsoft.mrtk.graphicstools.unity` (0.8.0)
- `com.microsoft.mrtk.tts.windows` (1.0.4)
- `com.microsoft.spatialaudio.spatializer.unity` (2.0.55)
- All `com.unity.*` official packages (URP, Input System, TextMeshPro, etc.)

If package resolution fails, check your network or proxy configuration.

## Repository Structure

```
.
├── org.mixedrealitytoolkit.accessibility/      # MRTK3 - Accessibility
├── org.mixedrealitytoolkit.audio/              # MRTK3 - Audio effects
├── org.mixedrealitytoolkit.core/               # MRTK3 - Core
├── org.mixedrealitytoolkit.data/               # MRTK3 - Data binding & theming
├── org.mixedrealitytoolkit.diagnostics/        # MRTK3 - Diagnostics
├── org.mixedrealitytoolkit.extendedassets/      # MRTK3 - Extended assets
├── org.mixedrealitytoolkit.input/              # MRTK3 - Input
├── org.mixedrealitytoolkit.spatialmanipulation/ # MRTK3 - Spatial manipulation
├── org.mixedrealitytoolkit.standardassets/      # MRTK3 - Standard assets
├── org.mixedrealitytoolkit.tools/              # MRTK3 - Editor tools
├── org.mixedrealitytoolkit.uxcomponents/        # MRTK3 - UX Components (Canvas)
├── org.mixedrealitytoolkit.uxcomponents.noncanvas/ # MRTK3 - UX Components (Non-Canvas)
├── org.mixedrealitytoolkit.uxcore/             # MRTK3 - UX Core
├── org.mixedrealitytoolkit.windowsspeech/       # MRTK3 - Windows Speech
│
├── UnityProjects/
│   └── MRTKDevTemplate/                        # ★ Unity project root
│       ├── Assets/
│       │   ├── Scripts/        # Custom scripts (189 C# files)
│       │   │   ├── Backend/    #   Backend / networking
│       │   │   ├── UI/         #   UI logic
│       │   │   ├── Selection/  #   Selection system
│       │   │   ├── CrossSections/ # Cross-section / clipping
│       │   │   ├── EyeTracking/#   Eye tracking
│       │   │   ├── Movement/   #   Motion control
│       │   │   ├── Pencil/     #   Drawing tools
│       │   │   └── ...
│       │   ├── Editor/         # Custom editor tools
│       │   ├── Scenes/         # 38 scenes
│       │   ├── Prefabs/        # Prefabs
│       │   ├── Models/         # 3D models (anatomy)
│       │   ├── Materials/      # Materials
│       │   ├── Shaders/        # Shaders
│       │   ├── Textures/       # Textures
│       │   ├── Plugins/        # Third-party plugins (Outline)
│       │   └── ImportedAssets/ # Imported tools & assets
│       ├── Packages/
│       │   └── manifest.json   # Package manifest
│       └── ProjectSettings/    # Unity project settings
│
├── Pipelines/                  # CI/CD pipeline configs
├── Tooling/                    # Dev tools & scripts
└── docs/                       # MRTK3 documentation
```

## MRTK3 Module Dependency Graph

```
core ───── input ──── spatialmanipulation ───── uxcore ───── uxcomponents
       │                                    │           └── uxcomponents.noncanvas
       ├── audio                            │
       ├── diagnostics                      │
       ├── data ─────────────────────────── (optional)
       ├── accessibility
       ├── windowsspeech
       └── tools

standardassets ── extendedassets
```

## License

MRTK3 is licensed under the [BSD 3-Clause License](./LICENSE.md).
