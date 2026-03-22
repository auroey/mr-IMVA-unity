# Research and Development Process

The research and development of the MedXR platform followed a structured methodology, transitioning from architectural design to functional implementation and performance optimization. This process ensured that the complex requirements of medical visualization were met while maintaining high performance and cross-platform compatibility.

## 1. Project Planning & Design
The initial phase focused on defining the software architecture to handle high-fidelity anatomical data.
- **Logic Framework**: A component-based architecture was adopted, utilizing singleton managers for centralized state control. The `GlobalVariables.cs` script was designed as the core backbone, maintaining references to all anatomical categories (Bones, Muscles, Nerves, Viscera, etc.) and defining global visual properties such as highlighting colors and UI themes.
- **Data Structure**: To manage the complex hierarchy of human anatomy, a custom parsing system (`MuscleGroups.cs`) was developed. This system reads external `TextAsset` files to dynamically construct relationships between muscles and their insertion points at runtime, allowing for flexible data updates without code recompilation.

## 2. Project & XR Basic Configuration
The development environment was configured to support high-performance XR rendering.
- **Engine & Version**: The project was built using **Unity 2021.3.45f2**, selected for its stability and compatibility with the target XR plugins.
- **Rendering Pipeline**: The **Universal Render Pipeline (URP)** version 12.1.15 was chosen to balance visual fidelity with performance, essential for rendering thousands of distinct anatomical meshes on standalone headsets.
- **XR Framework**: **Mixed Reality Toolkit 3 (MRTK3)** was integrated alongside **OpenXR** (v1.13.0) and **AR Foundation**, providing a standardized input system and robust spatial interaction components compatible with HoloLens 2, Meta Quest, and other OpenXR-compliant devices.

## 3. Scene & Interaction Prototype
A prototype phase established the fundamental interaction mechanics.
- **XR Origin**: The scene architecture utilizes a standard XR Origin setup, modified to support both hand-tracking and controller inputs.
- **Selection Mechanics**: A custom raycasting system was implemented to handle precise selection of small anatomical parts. This evolved into the `SelectedObjectsManagement.cs` system, which manages the selection state, handling multi-object selection via box and lasso tools defined in `ActionControl.cs`.

## 4. Content Production & Function Implementation
Core features were developed to enable detailed medical analysis.
- **Advanced Interaction Tools**: The `ActionControl.cs` script orchestrates a suite of analysis tools, including:
    - **Isolation Mode**: Implemented in `MeshManagement.cs`, allowing users to isolate specific organs or muscle groups while ghosting or hiding surrounding tissue.
    - **Annotation System**: A "Local/Global Note" feature enables users to place 3D spatial anchors on specific anatomical structures for educational tagging.
    - **Explosion/Cross-Section**: Dynamic gizmos were integrated to allow users to translate parts (`TranslateObject.cs`) or view cross-sections, facilitating the study of internal structures.
- **Command Pattern**: An Undo/Redo system (`CommandController.cs`) was implemented to track user actions (selection, deletion, visibility changes), essential for a non-destructive educational workflow.

## 5. Performance & Experience Optimization
Given the high polygon count of the anatomical models, significant optimization was required.
- **Mesh Management**: The `MeshManagement.cs` system efficiently handles the rendering state of thousands of objects. It utilizes shared materials and batching where possible to reduce draw calls.
- **Visibility Culling**: A hierarchical visibility system was developed to cull internal structures when not exposed, significantly improving frame rates.
- **Material Instancing**: Custom shaders and material property blocks were used to change object colors (e.g., for highlighting) without creating new material instances, preventing memory bloat.

## 6. Multi-platform Build & Testing
The final phase focused on deployment and stability.
- **Build Configuration**: Separate build profiles were created for Standalone Windows and Android (Quest/Pico), ensuring asset bundles and texture compression settings (ASTC for Android) were optimized for each platform.
- **Reset Logic**: A robust `SystemResetManager.cs` was implemented to restore the application to its initial state without reloading the scene, allowing for rapid reuse in classroom rotations.
