# MedXR Research and Development Process

This document outlines the technical research and development process for the MedXR project, detailing the architecture, data management strategies, and interaction patterns established during development.

## 1. Technical Architecture & Foundation

The project was architected using **Unity 2021.3.45f2 (LTS)** to ensure long-term stability and compatibility across devices. The core interaction framework is built upon **Mixed Reality Toolkit 3 (MRTK3)**, leveraging OpenXR to support a wide range of XR devices (Hololens 2, Meta Quest, etc.).

### Key Components:
-   **Engine**: Unity 2021.3.45f2
-   **XR Framework**: MRTK3 (`org.mixedrealitytoolkit.*` packages) via OpenXR.
-   **Render Pipeline**: Universal Render Pipeline (URP) for optimized performance on mobile mobile/standalone VR chipsets.

## 2. Core Logic & Interaction Design

The central nervous system of the application is the **Management** layer, specifically the `NamesManagement` system, which orchestrates the complex relationship between 3D anatomical models and their medical definitions.

### "Click-to-Identify" Workflow
We developed a robust object identification system (`NamesManagement.cs`) that handles the complexities of medical anatomy hierarchies:
1.  **Object Recognition**: The system parses 3D object names, intelligently stripping suffixes (e.g., "(R)", "(L)", ".t") to identify the canonical body part regardless of its left/right laterality.
2.  **Visual Selection**: Integrated with `SelectedObjectsManagement` to provide immediate visual feedback (highlighting/isolation) when a user interacts with a body part.
3.  **Camera Automation**: A `CameraController` script automatically focuses and frames the selected anatomical structure to ensure optimal viewing angles.

### Annotation Tools
To support educational use cases, we implemented a `Pencil` system allowing users to draw directly in 3D space, turning the anatomical model into an interactive whiteboard.

## 3. Data Management Strategy (Hybrid Offline-First)

A significant R&D challenge was managing multi-language medical definitions while ensuring the application remained functional without an internet connection. We adopted a **Hybrid Offline-First** approach.

### Local Data (Primary)
-   **Architecture**: `ReadLocalDefinitions.cs` serves as the primary data provider.
-   **Implementation**: Medical descriptions are stored in `TextAsset` files (text/CSV) embedded within the application.
-   **Multi-language Support**: A custom parsing engine reads these assets, capable of extracting specific language blocks (English, Spanish, French, Portuguese) using custom delimiters (e.g., `;;;ES;;;`, `;;;FR;;;`). This allows for instant language switching without network calls.

### Cloud Integration (Secondary/Expansion)
-   **Backend Logic**: `ReadDB.cs` was architected to support cloud extensibility.
-   **Features**:
    -   **Firebase Firestore**: Hooks are in place to fetch updated descriptions from a cloud database, allowing content updates without app store releases.
    -   **Wikipedia API**: An experimental `ReadWikipedia` module was prototyped to dynamically fetch encyclopedic context for selected anatomy, parsing JSON responses from the MediaWiki API.

## 4. Localization System
We developed a custom localization pipeline within `NamesManagement.cs`:
-   **Dictionary Mapping**: On initialization, the system loads a master translation file into a `Dictionary<string, string[]>`.
-   **Runtime Swapping**: When a language is changed, the system iterates through all active `NameAndDescription` components in the scene, dynamically updating their display names and associated metadata in real-time.

## 5. UI/UX Patterns
The UI was built using **TextMeshPro** for crisp text rendering in VR/AR. Custom UI scripts in `Assets/Scripts/UI` manage complex interactions like:
-   **Contextual Menus**: Right-click/hold interactions trigger context-sensitive options.
-   **Responsive Layouts**: Descriptions are displayed in a `ScrollView` that automatically resizes and adjusts margins based on the content length and presence of warnings.
