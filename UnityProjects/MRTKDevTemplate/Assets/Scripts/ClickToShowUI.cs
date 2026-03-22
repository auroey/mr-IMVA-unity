// Copyright (c) Mixed Reality Toolkit Contributors
// Licensed under the BSD 3-Clause

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using MixedReality.Toolkit;
using TMPro;

/// <summary>
/// Shows or toggles a UI when the object is clicked. Requires StatefulInteractable on the same object (or assigned).
/// </summary>
[AddComponentMenu("Scripts/MRTK/Click To Show UI")]
public class ClickToShowUI : MonoBehaviour
{
    /// <summary>
    /// Static default description library. Key = organ name (PascalCase), Value = default description in English.
    /// </summary>
    private static readonly Dictionary<string, string> DefaultOrganDescriptions = new Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase)
    {
        { "SensoryOrgans", "Collects environmental stimuli (light, sound, chemical, mechanical) and converts them into neural signals." },
        { "CentralNervousSystem", "The body's command center, composed of the brain and spinal cord. Integrates sensory information and issues motor commands." },
        { "PeripheralNervousSystem", "Connects the central nervous system to the limbs and viscera, facilitating bidirectional information transfer." },
        { "RespiratoryApparatus", "Executes gas exchange, supplying the body with oxygen and eliminating carbon dioxide." },
        { "ThoracicViscera", "Contains vital organs such as the heart and lungs, responsible for circulation and respiration." },
        { "EndocrineSystem", "Regulates metabolism, growth, and reproduction through hormone secretion." },
        { "DigestiveSystem", "Responsible for the physical and chemical breakdown of food, nutrient absorption, and waste excretion." },
        { "AbdominopelvicViscera", "Contains the major abdominal and pelvic organs of the digestive, urinary, and reproductive systems." },
        { "ReproductiveSystem", "Responsible for producing reproductive cells, secreting sex hormones, and maintaining species continuity." },
        { "UrinarySystem", "Filters blood to produce urine, maintaining fluid and acid-base balance." },
        { "VertebralColumn", "Supports the trunk, protects the spinal cord, and allows for flexible, multi-axis movement." },
        { "Dentition", "Assists in the mastication of food, speech articulation, and maintaining facial structure." },
        { "FacialSkeleton", "Forms the facial contour, supports sensory organs, and provides attachment points for muscles of mastication." },
        { "Neurocranium", "Forms a rigid cranial cavity to protect the brain and supports visual and auditory organs." },
        { "UpperLimbSkeleton", "Acts as a lever system supporting grasping, load-bearing, and fine motor skills." },
        { "LowerLimbSkeleton", "The structural framework for the lower body, maintaining balance and supporting locomotion." },
        { "NasalCartilages", "Maintains the shape of the nasal cavity, assists in respiration, and filters inhaled air." },
        { "PelvicGirdle", "Connects the trunk to the lower limbs, bears upper body weight, and protects pelvic viscera." },
        { "LowerLimbMuscles", "Drives lower limb joint movement, providing power for standing and mobility." },
        { "PelvicMuscles", "Supports pelvic viscera and regulates movements of the thigh and trunk." },
        { "ThoracicMuscles", "Includes respiratory muscles (e.g., intercostals) that assist in the expansion and contraction of the thoracic cage." },
        { "UpperLimbMuscles", "Controls precise movements of the shoulder, arm, and hand for grasping and fine manipulation." },
        { "BackMuscles", "Maintains spinal stability and enables extension, lateral flexion, and rotation of the trunk." },
        { "NeckMuscles", "Supports head weight and allows for multidimensional spatial movement of the head." },
        { "FacialMuscles", "Controls facial expressions, mastication, and assists in closing the eyelids and lips." },
        { "AbdominalMuscles", "Protects abdominal viscera, maintains intra-abdominal pressure, and assists in trunk flexion and balance." }
    };

    [Tooltip("要显示/隐藏的 UI（Canvas 或 Panel 等 GameObject）")]
    [SerializeField]
    private GameObject uiPanel;

    [Tooltip("可选：显示描述的 TextMeshPro。")]
    [SerializeField]
    private TMP_Text descriptionText;

    [Tooltip("器官显示名。留空则从物体名自动取（并去掉末尾的 Controller）。")]
    [SerializeField]
    private string organDisplayName;

    [Tooltip("器官功能描述，格式示例：负责泵血，将血液输送到全身。")]
    [SerializeField]
    [TextArea(2, 4)]
    private string organDescription;

    [Tooltip("为 true 时：每次点击在显示/隐藏之间切换；为 false 时：每次点击只显示 UI")]
    [SerializeField]
    private bool toggleMode = true;

    [Tooltip("若为空则使用同物体上的 StatefulInteractable")]
    [SerializeField]
    private StatefulInteractable interactable;

    private void Awake()
    {
        if (interactable == null)
        {
            interactable = GetComponent<StatefulInteractable>();
        }

        if (interactable == null)
        {
            Debug.LogWarning("ClickToShowUI: 未找到 StatefulInteractable，请在该物体上添加 StatefulInteractable 或指定 Interactable。", this);
            return;
        }

        if (uiPanel != null && !toggleMode)
        {
            uiPanel.SetActive(false);
        }

        interactable.OnClicked.AddListener(OnObjectClicked);
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.OnClicked.RemoveListener(OnObjectClicked);
        }
    }

    private void OnObjectClicked()
    {
        if (descriptionText != null)
        {
            descriptionText.text = GetDescriptionText();
        }

        if (uiPanel == null)
        {
            return;
        }

        if (toggleMode)
        {
            uiPanel.SetActive(!uiPanel.activeSelf);
        }
        else
        {
            uiPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Gets the description text to display. Splits CamelCase for display, uses raw key for dictionary lookup.
    /// </summary>
    private string GetDescriptionText()
    {
        // 1. 获取原始 Key（大驼峰，如 "UpperLimbSkeleton"）
        string rawKey = string.IsNullOrWhiteSpace(organDisplayName)
            ? GetNameWithoutController(gameObject.name)
            : organDisplayName.Trim();

        // 2. 将大驼峰转换为带空格的显示名称（如 "Upper Limb Skeleton"）
        string displayName = SplitCamelCase(rawKey);

        string description;

        // 3. 匹配描述（使用原始无空格的 rawKey 进行字典匹配）
        if (!string.IsNullOrWhiteSpace(organDescription))
        {
            description = organDescription.Trim();
        }
        else if (DefaultOrganDescriptions.TryGetValue(rawKey, out string defaultDesc))
        {
            description = defaultDesc;
        }
        else
        {
            return displayName; // 如果没有对应描述，仅返回拆分后的名称
        }

        return $"{displayName}: {description}";
    }

    /// <summary>
    /// Removes the "_Controller" suffix from the object name.
    /// </summary>
    private static string GetNameWithoutController(string objectName)
    {
        if (string.IsNullOrEmpty(objectName)) return objectName;
        const string suffix = "_Controller";
        if (objectName.Length > suffix.Length &&
            objectName.EndsWith(suffix, System.StringComparison.OrdinalIgnoreCase))
        {
            return objectName.Substring(0, objectName.Length - suffix.Length).TrimEnd();
        }
        return objectName;
    }

    /// <summary>
    /// Splits PascalCase/CamelCase strings into words with spaces. (e.g., "SensoryOrgans" -> "Sensory Organs")
    /// </summary>
    private static string SplitCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        
        // 使用正则：在每个大写字母前加一个空格（跳过第一个字符）
        return Regex.Replace(input, "(?<!^)([A-Z])", " $1").Trim();
    }

    public void HideUI()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    public void ShowUI()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
    }
}