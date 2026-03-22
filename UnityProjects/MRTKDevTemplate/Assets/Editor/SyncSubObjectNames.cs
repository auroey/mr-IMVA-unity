using UnityEngine;
using UnityEditor;

public class SyncSubObjectNames : EditorWindow
{
    [MenuItem("Tools/Hierarchy/Sync Sub-Object Names")]
    public static void SyncNames()
    {
        // 1. 定义四大系统根物体
        string[] rootNames = { "NervousSystem", "VisceralSystem", "SkeletalSystem", "MuscularSystem" };
        int count = 0;

        foreach (string rootName in rootNames)
        {
            GameObject root = GameObject.Find(rootName);
            if (root == null) continue;

            // 2. 遍历每一个 _Controller
            foreach (Transform controller in root.transform)
            {
                if (controller.name.EndsWith("_Controller"))
                {
                    // 获取目标名字（例如从 "ReproductiveSystem_Controller" 提取出 "ReproductiveSystem"）
                    string targetName = controller.name.Replace("_Controller", "");

                    // 3. 遍历 Controller 的直接子物体
                    foreach (Transform child in controller)
                    {
                        // 逻辑：我们只针对包含 .g, .s, .j 或原本带有 system 字样的模型容器进行重命名
                        // 这样可以避免误伤到可能存在的 UI 或其他辅助物体
                        if (child.name.Contains(".g") || child.name.Contains(".s") || 
                            child.name.Contains(".j") || child.name.Contains("systems"))
                        {
                            if (child.name != targetName)
                            {
                                Undo.RecordObject(child.gameObject, "Rename Sub-Object");
                                child.name = targetName;
                                count++;
                            }
                        }
                    }
                }
            }
        }

        Debug.Log($"[层级同步] 完成！已将 {count} 个子物体重命名为与其父物体匹配的名称。");
    }
}