using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

[RequireComponent(typeof(Image), typeof(Button))]
public class ButtonColorAnimator : MonoBehaviour
{
    [SerializeField] private Sprite _selectedAnswerButtonSprite;
    [SerializeField] private Sprite _correctAnswerButtonSprite;
    [SerializeField] private Sprite _wrongAnswerButtonSprite;
    [SerializeField] private float _animationDelay;

    private SignalBus _signalBus;
    private Image _buttonImage;
    private Button _thisButton;
    private Sprite _startButtonSprite;
    private bool _isButtonTouched;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }


    private void Awake()
    {
        _buttonImage = GetComponent<Image>();

        _thisButton = GetComponent<Button>();

        _startButtonSprite = _buttonImage.sprite;

        _thisButton.onClick.AddListener(DrawButtonTouch);

        _signalBus.Subscribe<PlayerAnswerCorrectSignal>(OnDrawCorrectAnswer);
        _signalBus.Subscribe<PlayerAnswerWrongSignal>(OnDrawWrongAnswer);
    }

    public void OnDrawCorrectAnswer()
    {
        if (_isButtonTouched)
        {
            _buttonImage.sprite = _correctAnswerButtonSprite;

            Sequence _animationSequence = DOTween.Sequence();

            _animationSequence.AppendInterval(_animationDelay)
                .AppendCallback(() => _isButtonTouched = false)
                .AppendCallback(() => _buttonImage.sprite = _startButtonSprite)
                .AppendCallback(() => _signalBus.Fire<SelectNewQuizItemSignal>());
        }
    }

    public void OnDrawWrongAnswer()
    {
        if (_isButtonTouched)
        {
            _buttonImage.sprite = _wrongAnswerButtonSprite;

            Sequence _animationSequence = DOTween.Sequence();

            _animationSequence.AppendInterval(_animationDelay)
                .AppendCallback(() => _isButtonTouched = false)
                .AppendCallback(() => _buttonImage.sprite = _startButtonSprite)
                .AppendCallback(() => _signalBus.Fire<PunishPlayerSignal>());
        }
    }

    public void DrawButtonTouch()
    {
        _isButtonTouched = true;

        _signalBus.Fire<FreezeGameSignal>();

        Sequence _animationSequence = DOTween.Sequence();

        _buttonImage.sprite = _selectedAnswerButtonSprite;

        _animationSequence.AppendInterval(_animationDelay).AppendCallback(() => _signalBus.Fire<DetermineCorrectAnswerSignal>());
    }

    private void OnDestroy()
    {
        _thisButton.onClick.RemoveListener(DrawButtonTouch);

        _signalBus.Unsubscribe<PlayerAnswerCorrectSignal>(OnDrawCorrectAnswer);
        _signalBus.Unsubscribe<PlayerAnswerWrongSignal>(OnDrawWrongAnswer);
    }
}
