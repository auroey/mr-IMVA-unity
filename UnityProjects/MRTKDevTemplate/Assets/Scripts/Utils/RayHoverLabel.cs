using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text.RegularExpressions; // 必须引入正则命名空间

public class RayHoverLabel : MonoBehaviour
{
    [Header("UI 引用")]
    public TextMeshPro labelText; 
    public string defaultMessage = "请指向部位";

    [Header("射线引用")]
    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

    void Update()
    {
        if (!UpdateTextContent(rightRay))
        {
            if (!UpdateTextContent(leftRay))
            {
                labelText.text = defaultMessage;
            }
        }

        // // 2. 视角对齐逻辑：让文字始终与视角平行
        // if (Camera.main != null)
        // {
        //     labelText.transform.rotation = Camera.main.transform.rotation;
        // }
    }

    bool UpdateTextContent(XRRayInteractor interactor)
    {
        if (interactor == null) return false;

        if (interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj != null)
            {
                string cleanName = hitObj.name
                    .Replace("_Controller", "")
                    .Replace(".g", "")
                    .Replace(".s", "")
                    .Replace(".j", "");
                
                // 核心修改：将提取出的干净名称进行驼峰拆解
                labelText.text = SplitCamelCase(cleanName);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 将大驼峰命名拆分为带空格的格式 (例如 "UpperLimb" -> "Upper Limb")
    /// </summary>
    private static string SplitCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return Regex.Replace(input, "(?<!^)([A-Z])", " $1").Trim();
    }
}