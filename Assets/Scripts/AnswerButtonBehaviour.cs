using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

[RequireComponent(typeof(Button))]
public class AnswerButtonBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;

    private SignalBus _signalBus;
    private Button _answerButton;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _answerButton = GetComponent<Button>();
        
        _answerButton.onClick.AddListener(() => SelectCurrentAnswer());
    }

    public void SelectCurrentAnswer()
    {
        _signalBus.Fire(new PlayerAnswerSelectedSignal(_buttonText.text));
    }

    private void OnDestroy()
    {
        _answerButton.onClick.RemoveListener(() => SelectCurrentAnswer());
    }
}
