using UnityEngine;
using Zenject;
using DG.Tweening;

public class QuizImageAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _quizImage;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Start()
    {
        _signalBus.Subscribe<SelectNewQuizItemSignal>(DrawQuizImageAnimation);
    }

    public void DrawQuizImageAnimation()
    {
        _quizImage.localScale = new Vector3(0, 0, 0);

        Sequence _sequence = DOTween.Sequence();

        _sequence.Append(_quizImage.DOScale(Vector3.one, 0.3f));
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SelectNewQuizItemSignal>(DrawQuizImageAnimation);
    }
}
