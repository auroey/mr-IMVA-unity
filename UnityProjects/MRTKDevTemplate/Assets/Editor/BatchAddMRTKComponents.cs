using UnityEngine;
using UnityEditor;
// 核心 MRTK3 命名空间
using MixedReality.Toolkit;
using MixedReality.Toolkit.SpatialManipulation;

public class BatchAddMRTKComponents : EditorWindow
{
    [MenuItem("Tools/MRTK/Add Core Interactive Components")]
    public static void AddComponentsWithLinks()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("请先在层级面板选中根物体（如 MuscularSystem100）。");
            return;
        }

        int count = 0;

        foreach (GameObject root in selectedObjects)
        {
            // 找到所有以 _Controller 结尾的子物体
            Transform[] allChildren = root.GetComponentsInChildren<Transform>(true);
            
            foreach (Transform t in allChildren)
            {
                if (t.name.EndsWith("_Controller"))
                {
                    GameObject obj = t.gameObject;

                    // 1. 添加并获取 Constraint Manager
                    ConstraintManager cm = TryAddComponent<ConstraintManager>(obj);

                    // 2. 添加并设置 Bounds Control
                    BoundsControl bc = TryAddComponent<BoundsControl>(obj);
                    if (bc != null && cm != null)
                    {
                        // 自动关联红圈中的 Constraints Manager 引用
                        bc.ConstraintsManager = cm; 
                    }

                    // 3. 添加并设置 Object Manipulator
                    ObjectManipulator om = TryAddComponent<ObjectManipulator>(obj);
                    if (om != null)
                    {
                        // 自动关联红圈中的 Host Transform 引用
                        om.HostTransform = obj.transform;
                    }

                    // 4. 添加其余不报错的组件
                    TryAddComponent<MinMaxScaleConstraint>(obj);
                    
                    // StatefulInteractable 通常在 MixedReality.Toolkit 命名空间下
                    // 如果这行也报错，可以先注释掉手动加
                    TryAddComponent<StatefulInteractable>(obj);

                    count++;
                }
            }
        }
        Debug.Log($"处理完成！已为 {count} 个 Controller 关联了 Host Transform 和 Constraint Manager。");
    }

    private static T TryAddComponent<T>(GameObject target) where T : Component
    {
        T component = target.GetComponent<T>();
        if (component == null)
        {
            component = Undo.AddComponent<T>(target);
        }
        return component;
    }
}