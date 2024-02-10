using Zenject;

public class PlayerAnswerVerifier : IInitializable, ILateDisposable
{
    private EventBus _eventBus;

    private string _correctAnswer;
    private string _playerAnswer;

    [Inject]
    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void Initialize()
    {
        _eventBus.OnNewQuizItemSelected += GetCorrectAnswer;
        _eventBus.OnPlayerSelectAnswer += GetPlayerAnswer;
    }

    public void LateDispose()
    {
        _eventBus.OnNewQuizItemSelected -= GetCorrectAnswer;
        _eventBus.OnPlayerSelectAnswer -= GetPlayerAnswer;
    }

    public void GetCorrectAnswer(QuizItem quizItem)
    {
        _correctAnswer = quizItem.CorrectAnswer;
    }

    public void GetPlayerAnswer(string playerAnswer)
    {
        _playerAnswer = playerAnswer;

        VerifyPlayerAnswer();
    }

    public void VerifyPlayerAnswer()
    {
        if(_playerAnswer == _correctAnswer)
        {
            _eventBus.OnPlayerAnswerCorrect?.Invoke();
        }
        else
        {
            _eventBus.OnPlayerAnswerIncorrect?.Invoke();
        }
    }
}
