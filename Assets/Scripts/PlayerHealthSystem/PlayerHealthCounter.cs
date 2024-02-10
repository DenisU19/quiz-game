using Zenject;

public class PlayerHealthCounter : IInitializable, ILateDisposable
{
    private PlayerHealthConfigs _playerHealthConfigs;
    private EventBus _eventBus;

    private int _allHealthCount;

    private int _currentHealthCount;

    public PlayerHealthCounter(PlayerHealthConfigs playerHealthConfigs, EventBus eventBus)
    {
        _playerHealthConfigs = playerHealthConfigs;
        _eventBus = eventBus;
    }
    public void Initialize()
    {
        _eventBus.OnPlayerAnswerIncorrect += ReduceHealth;

        _allHealthCount = _playerHealthConfigs.HealthCount;

        _currentHealthCount = _allHealthCount;
    }

    public void ReduceHealth()
    {
        _currentHealthCount -= _playerHealthConfigs.HealthDamage;

        _eventBus.OnHealthCountReduced?.Invoke(_currentHealthCount);

        if(_currentHealthCount == 0)
        {
            _eventBus.OnGameOver?.Invoke();
        }
    }

    public void LateDispose()
    {
        _eventBus.OnPlayerAnswerIncorrect -= ReduceHealth;

    }
}
