using UnityEngine;
using UnityEditor;
using TMPro;

public class BulkLinkUIScript : EditorWindow
{
    [MenuItem("Tools/MRTK/Bulk Link Description UI")]
    public static void LinkUI()
    {
        // 1. 获取选中的根物体
        GameObject[] roots = Selection.gameObjects;
        if (roots.Length == 0)
        {
            Debug.LogWarning("请先在层级面板选中根物体（如 MuscularSystem100）。");
            return;
        }

        // 2. 精准寻找场景中的 UI 组件
        // 寻找父容器 DescriptionPanel 作为要显示/隐藏的面板
        GameObject parentPanel = GameObject.Find("DescriptionPanel");
        
        // 寻找名为 description 的子物体
        GameObject textObj = null;
        if (parentPanel != null)
        {
            Transform t = parentPanel.transform.Find("description");
            if (t != null) textObj = t.gameObject;
        }
        else
        {
            // 如果找不到父容器，则尝试全局搜索名为 description 的物体
            textObj = GameObject.Find("description");
        }

        if (textObj == null)
        {
            Debug.LogError("未能找到名为 'description' 的物体，请检查层级名是否拼写正确。");
            return;
        }

        TMP_Text targetText = textObj.GetComponent<TMP_Text>();
        // 将父容器设为 uiPanel，这样点击时整个面板都会显示/隐藏
        GameObject targetPanel = (parentPanel != null) ? parentPanel : textObj;

        // 3. 执行批量赋值
        int count = 0;
        foreach (GameObject root in roots)
        {
            ClickToShowUI[] scripts = root.GetComponentsInChildren<ClickToShowUI>(true);

            foreach (var script in scripts)
            {
                SerializedObject so = new SerializedObject(script);
                
                // 批量修改 private 变量
                so.FindProperty("uiPanel").objectReferenceValue = targetPanel;
                so.FindProperty("descriptionText").objectReferenceValue = targetText;

                so.ApplyModifiedProperties();
                count++;
            }
        }

        Debug.Log($"处理完成！已为 {count} 个组件绑定 UI。Panel: {targetPanel.name}, Text: {textObj.name}");
    }
}