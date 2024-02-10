using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizViewDrawer : MonoBehaviour
{
    [SerializeField] private EventBus _eventBus;
    [SerializeField] private Image _questionImage;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI[] _answerButtonsTexts;

    private System.Random _answerOrderRandom;

    private void Awake()
    {
        _eventBus.OnNewQuizItemSelected += DrawNewQuizView;

        _answerOrderRandom = new System.Random();
    }

    public void DrawNewQuizView(QuizItem quizItem)
    {
        _questionImage.sprite = quizItem.QuestionImage;

        _questionText.text = quizItem.Question;

        ShakeButtonsTexts();

        RedrawButtonsText(quizItem.AllAnswers);

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
        _eventBus.OnNewQuizItemSelected -= DrawNewQuizView;
    }
}
