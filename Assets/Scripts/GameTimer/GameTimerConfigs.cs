using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/TimerConfigs", fileName = "TimerConfigs")]
public class GameTimerConfigs : ScriptableObject
{
    [SerializeField] private float _allPlayTime;
    [SerializeField] private float _timeSpendSpeed;
    [SerializeField] private float _addedTime;
    [SerializeField] private float _deductibleTime;

    public float AllPlayTime => _allPlayTime;
    public float TimeSpedSpeed => _timeSpendSpeed;
    public float AddedTime => _addedTime;
    public float DeductibleTime => _deductibleTime;
}
