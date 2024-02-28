using UnityEngine;
using Zenject;

public class GameTimerManager : IInitializable, ITickable, ILateDisposable
{
    private GameTimerConfigs _gameTimerConfigs;

    private GameTimerViewDrawer _gameTimerViewDrawer;
    private SignalBus _signalBus;

    private float _allPlayTime;
    private float _currentTime;

    private bool _isTimerActive = true;

    public GameTimerManager(GameTimerConfigs gameTimerConfigs, GameTimerViewDrawer gameTimerViewDrawer, SignalBus signalBus)
    {
        _gameTimerConfigs = gameTimerConfigs;
        _gameTimerViewDrawer = gameTimerViewDrawer;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<FreezeGameSignal>(OnTimerStop);
        _signalBus.Subscribe<SelectNewQuizItemSignal>(OnTimerStart);
        _signalBus.Subscribe<PlayerAnswerCorrectSignal>(OnTimeAdd);
        _signalBus.Subscribe<PunishPlayerSignal>(OnTimeSubtract);

        _allPlayTime = _gameTimerConfigs.AllPlayTime;
        _currentTime = _allPlayTime;
    }

    public void Tick()
    {
        if (_isTimerActive)
        {
            _currentTime -= Time.deltaTime * _gameTimerConfigs.TimeSpedSpeed;

            CheckTimeOver();

            _gameTimerViewDrawer.RedrawTimerView(_currentTime / _allPlayTime);
        }
    }

    public void CheckTimeOver()
    {
        if (_currentTime <= 0)
        {
            _currentTime = 0;
            _signalBus.Fire<GameOverSignal>();
            _isTimerActive = false;
        }
    }

    public void OnTimeAdd()
    {
        _currentTime += _gameTimerConfigs.AddedTime;
    }

    public void OnTimeSubtract()
    {
        _currentTime -= _gameTimerConfigs.DeductibleTime;

        CheckTimeOver();

        _gameTimerViewDrawer.RedrawTimerView(_currentTime / _allPlayTime);

        if (_currentTime > 0)
        {
            _signalBus.Fire<SelectNewQuizItemSignal>();
        }
    }

    public void OnTimerStop()
    {
        _isTimerActive = false;
    }

    public void OnTimerStart()
    {
        _isTimerActive = true;
    }

    public void LateDispose()
    {
        _signalBus.Unsubscribe<FreezeGameSignal>(OnTimerStop);
        _signalBus.Unsubscribe<SelectNewQuizItemSignal>(OnTimerStart);
        _signalBus.Unsubscribe<PlayerAnswerCorrectSignal>(OnTimeAdd);
        _signalBus.Unsubscribe<PunishPlayerSignal>(OnTimeSubtract);
    }
}
