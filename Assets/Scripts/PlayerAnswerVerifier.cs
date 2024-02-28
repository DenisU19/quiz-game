using Zenject;

public class PlayerAnswerVerifier : IInitializable, ILateDisposable
{
    private SignalBus _signalBus;

    private string _correctAnswer;
    private string _playerAnswer;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<QuizItemSelectedSignal>(OnGetCorrectAnswer);
        _signalBus.Subscribe<PlayerAnswerSelectedSignal>(OnGetPlayerAnswer);
        _signalBus.Subscribe<DetermineCorrectAnswerSignal>(OnVerifiedPlayerAnswer);
    }

    public void OnGetCorrectAnswer(QuizItemSelectedSignal signal)
    {
        _correctAnswer = signal.NewQuizItem.CorrectAnswer;
    }

    public void OnGetPlayerAnswer(PlayerAnswerSelectedSignal signal)
    {
        _playerAnswer = signal.PlayerAnswer;
    }

    public void OnVerifiedPlayerAnswer()
    {
        if(_playerAnswer == _correctAnswer)
        {
            _signalBus.Fire<PlayerAnswerCorrectSignal>();
        }
        else
        {
            _signalBus.Fire<PlayerAnswerWrongSignal>();
        }
    }

    public void LateDispose()
    {
        _signalBus.Unsubscribe<QuizItemSelectedSignal>(OnGetCorrectAnswer);
        _signalBus.Unsubscribe<PlayerAnswerSelectedSignal>(OnGetPlayerAnswer);
        _signalBus.Unsubscribe<DetermineCorrectAnswerSignal>(OnVerifiedPlayerAnswer);
    }
}
