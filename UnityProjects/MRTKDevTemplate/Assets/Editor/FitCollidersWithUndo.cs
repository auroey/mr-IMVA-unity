using UnityEngine;
using UnityEditor;

public class FitCollidersToSecondLayer : EditorWindow
{
    [MenuItem("Tools/Physics/Fit Combined Collider to Second Layer Only")]
    public static void FitColliders()
    {
        GameObject[] roots = Selection.gameObjects;

        if (roots.Length == 0)
        {
            Debug.LogWarning("请先选中根物体。");
            return;
        }

        int count = 0;

        foreach (GameObject root in roots)
        {
            // 1. 寻找第一层：xxx_Controller
            foreach (Transform controller in root.transform)
            {
                if (controller.name.EndsWith("_Controller"))
                {
                    // 2. 寻找第二层：Controller 的直接子物体
                    foreach (Transform secondLayer in controller)
                    {
                        // 3. 获取该物体下所有零件的 Renderer（包括自己和所有后代）
                        Renderer[] childRenderers = secondLayer.GetComponentsInChildren<Renderer>(true);

                        if (childRenderers.Length == 0) continue;

                        // 4. 计算合并后的 Bounds（世界空间坐标）
                        Bounds combinedBounds = childRenderers[0].bounds;
                        foreach (Renderer renderer in childRenderers)
                        {
                            combinedBounds.Encapsulate(renderer.bounds);
                        }

                        // 5. 添加或记录 BoxCollider
                        BoxCollider bc = secondLayer.gameObject.GetComponent<BoxCollider>();
                        if (bc == null)
                        {
                            bc = Undo.AddComponent<BoxCollider>(secondLayer.gameObject);
                        }
                        else
                        {
                            Undo.RecordObject(bc, "Update Combined BoxCollider");
                        }

                        // 6. 将世界空间的 Bounds 转换回该物体的 Local 空间
                        // 这一步非常关键，确保旋转和缩放后依然对齐
                        Vector3 localCenter = secondLayer.InverseTransformPoint(combinedBounds.center);
                        Vector3 localSize = combinedBounds.size;
                        
                        // 处理父物体缩放对尺寸的影响
                        Vector3 lossyScale = secondLayer.lossyScale;
                        localSize.x /= lossyScale.x;
                        localSize.y /= lossyScale.y;
                        localSize.z /= lossyScale.z;

                        bc.center = localCenter;
                        bc.size = localSize;
                        
                        count++;
                    }
                }
            }
        }

        Debug.Log($"处理完成！已为 {count} 个第二层物体适配了合并后的 BoxCollider。");
    }
}