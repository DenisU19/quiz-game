using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuizViewDrawer : MonoBehaviour
{
    [SerializeField] private Image _questionImage;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI[] _answerButtonsTexts;

    private SignalBus _signalBus;
    private System.Random _answerOrderRandom;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<QuizItemSelectedSignal>(OnDrawNewQuizView);

        _answerOrderRandom = new System.Random();
    }

    public void OnDrawNewQuizView(QuizItemSelectedSignal signal)
    {
        _questionImage.sprite = signal.NewQuizItem.QuestionImage;

        _questionText.text = signal.NewQuizItem.Question;

        ShakeButtonsTexts();

        RedrawButtonsText(signal.NewQuizItem.AllAnswers);

    }

    public void ShakeButtonsTexts()
    {
        _answerButtonsTexts = _answerButtonsTexts.OrderBy(r => _answerOrderRandom.Next()).ToArray();
    }

    public void RedrawButtonsText(string[] answersText)
    {
        for (int i = 0; i < _answerButtonsTexts.Length; i++)
        {
            _answerButtonsTexts[i].text = answersText[i];
        }
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<QuizItemSelectedSignal>(OnDrawNewQuizView);
    }
}
