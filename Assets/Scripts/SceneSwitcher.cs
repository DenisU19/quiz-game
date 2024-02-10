using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneSwitcher : MonoBehaviour
{
    private string _sceneName;
    private EventBus _eventBus;

    [Inject]
    private void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Awake()
    {
        _sceneName = SceneManager.GetActiveScene().name;

        _eventBus.OnGameRestarted += RestartScene;
    }
    public void RestartScene()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }

    private void OnDestroy()
    {
        _eventBus.OnGameRestarted -= RestartScene;
    }
}
