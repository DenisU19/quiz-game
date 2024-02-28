using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameTimerViewDrawer : MonoBehaviour
{
    [SerializeField] private Image _timerImage;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }


    public void RedrawTimerView(float currentTimeInPercent)
    {
        _timerImage.fillAmount = currentTimeInPercent;
    }
}
