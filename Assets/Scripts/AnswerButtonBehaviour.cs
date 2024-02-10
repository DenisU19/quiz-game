using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class AnswerButtonBehaviour : MonoBehaviour
{
    [SerializeField] private EventBus _eventBus;
    [SerializeField] private TextMeshProUGUI _buttonText;

    private Button _answerButton;


    private void Awake()
    {
        _answerButton = GetComponent<Button>();
        
        _answerButton.onClick.AddListener(() => SelectCurrentAnswer());
    }

    public void SelectCurrentAnswer()
    {
        _eventBus.OnPlayerSelectAnswer?.Invoke(_buttonText.text);
    }

    private void OnDestroy()
    {
        _answerButton.onClick.RemoveListener(() => SelectCurrentAnswer());
    }
}
