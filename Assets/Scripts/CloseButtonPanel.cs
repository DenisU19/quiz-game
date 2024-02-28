using UnityEngine;
using Zenject;

public class CloseButtonPanel : MonoBehaviour
{
    [SerializeField] private GameObject _closeButtonPanel;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    void Awake()
    {
        _signalBus.Subscribe<FreezeGameSignal>(OnPanelActivate);
        _signalBus.Subscribe<SelectNewQuizItemSignal>(OnPanelHide);

    }

    public void OnPanelActivate()
    {
        _closeButtonPanel.SetActive(true);
    }

    public void OnPanelHide()
    {
        _closeButtonPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<FreezeGameSignal>(OnPanelActivate);
        _signalBus.Unsubscribe<SelectNewQuizItemSignal>(OnPanelHide);
    }
}
