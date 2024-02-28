using System;
using Zenject;

public class PlayerHealthCounter : IInitializable, ILateDisposable
{
    private PlayerHealthConfigs _playerHealthConfigs;
    private PlayerHealthViewDrawer _playerHealthViewDrawer;
    private SignalBus _signalBus;

    private int _allHealthCount;

    private int _currentHealthCount;

    public Action<int> ReduceHealthImages;

    public PlayerHealthCounter(PlayerHealthConfigs playerHealthConfigs, SignalBus signalBus, PlayerHealthViewDrawer playerHealthViewDrawer)
    {
        _playerHealthConfigs = playerHealthConfigs;
        _signalBus = signalBus;
        _playerHealthViewDrawer = playerHealthViewDrawer;
        
    }
    public void Initialize()
    {
        _signalBus.Subscribe<PunishPlayerSignal>(OnReduceHealth);

        _allHealthCount = _playerHealthConfigs.HealthCount;

        _currentHealthCount = _allHealthCount;
    }

    public void OnReduceHealth()
    {
        _currentHealthCount -= _playerHealthConfigs.HealthDamage;

        _playerHealthViewDrawer.DrawLostHeath(_currentHealthCount);

        if (_currentHealthCount == 0)
        {
            _signalBus.Fire<GameOverSignal>();

            return;
        }

        _signalBus.Fire<SelectNewQuizItemSignal>();
    }

    public void LateDispose()
    {
        _signalBus.Unsubscribe<PunishPlayerSignal>(OnReduceHealth);
    }
}
