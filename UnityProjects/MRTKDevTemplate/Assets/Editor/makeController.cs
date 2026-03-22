using UnityEngine;
using UnityEditor;

public class AdvancedHierarchyTool : EditorWindow
{
    [MenuItem("Tools/Hierarchy/Wrap and Zero-out Transforms")]
    public static void WrapAndZeroTransform()
    {
        GameObject[] roots = Selection.gameObjects;

        if (roots.Length == 0)
        {
            Debug.LogWarning("请先选中根系统物体。");
            return;
        }

        int count = 0;

        foreach (GameObject root in roots)
        {
            // 备份子物体列表
            Transform[] children = new Transform[root.transform.childCount];
            for (int i = 0; i < root.transform.childCount; i++)
            {
                children[i] = root.transform.GetChild(i);
            }

            foreach (Transform child in children)
            {
                // 跳过已处理的或 Controller 物体
                if (child.name.EndsWith("_Controller")) continue;

                // 1. 记录子物体的当前变换数据
                Vector3 originalPos = child.localPosition;
                Quaternion originalRot = child.localRotation;
                Vector3 originalScale = child.localScale;

                // 2. 创建 Controller
                GameObject newController = new GameObject(child.name + "_Controller");
                Undo.RegisterCreatedObjectUndo(newController, "Create Controller");

                // 3. 设置 Controller 的变换（继承子物体原本的位、旋、缩）
                newController.transform.SetParent(root.transform);
                newController.transform.localPosition = originalPos;
                newController.transform.localRotation = originalRot;
                newController.transform.localScale = originalScale;

                // 4. 将子物体移入 Controller
                Undo.SetTransformParent(child, newController.transform, "Reparent");

                // 5. 归零子物体的局部变换
                child.localPosition = Vector3.zero;
                child.localRotation = Quaternion.identity;
                child.localScale = Vector3.one;

                count++;
            }
        }
        Debug.Log($"处理完成：已重构 {count} 个物体的层级并归零坐标。");
    }
}