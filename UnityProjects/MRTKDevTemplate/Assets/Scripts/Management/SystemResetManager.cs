using UnityEngine;
using UnityEngine.SceneManagement; // 必须添加这个命名空间来管理场景

public class SystemResetManager : MonoBehaviour
{
    // 虽然重载场景不需要这个数组了，但为了不破坏你 Inspector 面板里可能已经做好的引用，可以保留或删除
    // [HideInInspector] public GameObject[] systemRoots; 

    /// <summary>
    /// 核心逻辑：直接重新加载当前激活的场景
    /// </summary>
    public void ResetAll()
    {
        // 获取当前场景的名字
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        // 重新加载场景
        // 这会彻底重置所有物体位置、UI 状态、物理效果以及 MRTK 的交互状态
        SceneManager.LoadScene(currentSceneName);
        
        Debug.Log($"场景 {currentSceneName} 已重新加载。");
    }
}