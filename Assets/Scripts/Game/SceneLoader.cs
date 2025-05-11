using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 這個方法會被 Button 的 OnClick 呼叫
    public void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("場景名稱不可為空！");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
