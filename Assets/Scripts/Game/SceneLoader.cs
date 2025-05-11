using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // �o�Ӥ�k�|�Q Button �� OnClick �I�s
    public void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("�����W�٤��i���šI");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
