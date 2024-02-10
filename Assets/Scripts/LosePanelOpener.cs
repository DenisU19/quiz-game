using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LosePanelOpener : MonoBehaviour
{
    [SerializeField] private Image _losePanel;

    private EventBus _eventBus;

    [Inject]
    private void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Awake()
    {
        _eventBus.OnGameOver += ActivateLosePanel;
    }

    public void ActivateLosePanel()
    {
        _losePanel.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _eventBus.OnGameOver -= ActivateLosePanel;
    }
}
