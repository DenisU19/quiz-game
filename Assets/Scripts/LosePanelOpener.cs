using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LosePanelOpener : MonoBehaviour
{
    [SerializeField] private Image _losePanel;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<GameOverSignal>(OnActivateLosePanel);
    }

    public void OnActivateLosePanel()
    {
        _losePanel.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<GameOverSignal>(OnActivateLosePanel);

    }
}
