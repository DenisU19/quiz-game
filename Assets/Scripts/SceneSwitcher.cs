using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private string _sceneName;

    private void Awake()
    {
        _sceneName = SceneManager.GetActiveScene().name;
    }
    public void RestartScene()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }
}
