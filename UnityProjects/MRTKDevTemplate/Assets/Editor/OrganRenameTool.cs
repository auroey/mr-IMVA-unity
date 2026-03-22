using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ProfessionalAnatomyRenameTool : EditorWindow
{
    // 定义原始名字到专业名字的映射表
    private static readonly Dictionary<string, string> NameMap = new Dictionary<string, string>()
    {
        // 神经系统
        { "Sense organs", "SensoryOrgans" },
        { "Central nervous system", "CentralNervousSystem" },
        { "Peripheral nervous system", "PeripheralNervousSystem" },

        // 内脏系统
        { "Respiratory system.g", "RespiratoryApparatus" },
        { "Thoracic cavity.g", "ThoracicViscera" },
        { "Endocrine glands.g", "EndocrineSystem" },
        { "Digestive system", "DigestiveSystem" },
        { "Abdominopelvic cavity.g", "AbdominopelvicViscera" },
        { "Genital systems", "ReproductiveSystem" },
        { "Urinary system", "UrinarySystem" },

        // 骨骼系统
        { "Bones of vertebral column", "VertebralColumn" },
        { "Teeth.g", "Dentition" },
        { "Extracranial bones of head.g", "FacialSkeleton" },
        { "Cranium", "Neurocranium" },
        { "Bones of upper limb", "UpperLimbSkeleton" },
        { "Bones of lower limb", "LowerLimbSkeleton" },
        { "Nasal cartilages.g", "NasalCartilages" },
        { "Bony pelvis", "PelvicGirdle" },

        // 肌肉系统
        { "Muscular system of lower limb", "LowerLimbMuscles" },
        { "Pelvic part of muscular system.g", "PelvicMuscles" },
        { "Thoracic part of muscular system.g", "ThoracicMuscles" },
        { "Muscular system of upper limb.g", "UpperLimbMuscles" },
        { "Dorsal part of muscular system.g", "BackMuscles" },
        { "Cervical part of muscular system.g", "NeckMuscles" },
        { "Cranial part of muscular system", "FacialMuscles" },
        { "Abdominal part of muscular system", "AbdominalMuscles" }
    };

    [MenuItem("Tools/Hierarchy/Professional Anatomy Rename")]
    public static void RenameToProfessional()
    {
        string[] rootNames = { "NervousSystem", "VisceralSystem", "SkeletalSystem", "MuscularSystem" };
        int count = 0;

        foreach (string rootName in rootNames)
        {
            GameObject root = GameObject.Find(rootName);
            if (root == null) continue;

            foreach (Transform child in root.transform)
            {
                // 去掉已有的 _Controller 方便匹配 Key
                string currentName = child.name.Replace("_Controller", "");

                if (NameMap.ContainsKey(currentName))
                {
                    string newName = NameMap[currentName] + "_Controller";
                    if (child.name != newName)
                    {
                        Undo.RecordObject(child.gameObject, "Professional Rename");
                        child.name = newName;
                        count++;
                    }
                }
            }
        }
        Debug.Log($"[专业化重命名] 完成！已修改 {count} 个 Controller 名称。");
    }
}