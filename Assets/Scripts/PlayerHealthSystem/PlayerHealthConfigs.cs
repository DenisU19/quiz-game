using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="GameConfigs/PlayerHealthConfigs", fileName ="PlayerHealthConfigs")]
public class PlayerHealthConfigs : ScriptableObject
{
    [SerializeField] private Image _healthImage;
    [SerializeField] private int _healthCount;
    [SerializeField] private int _healthDamage;
    [SerializeField] private int _healthRecovery;

    public Image HealthImage => _healthImage;
    public int HealthCount => _healthCount;
    public int HealthDamage => _healthDamage;
    public int HealthRecovery => _healthRecovery;

}
