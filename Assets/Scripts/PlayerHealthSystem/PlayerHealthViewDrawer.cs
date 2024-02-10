using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHealthViewDrawer : MonoBehaviour
{
    [SerializeField] private Transform _healthImagesParent;

    private PlayerHealthConfigs _playerHealthConfigs;
    private EventBus _eventBus;
    private List<Image> _healthImageCollection;

    [Inject]
    private void Construct(PlayerHealthConfigs playerHealthConfigs, EventBus eventBus)
    {
        _playerHealthConfigs = playerHealthConfigs;
        _eventBus = eventBus;
    }

    private void Awake()
    {
        _eventBus.OnHealthCountReduced += ReduceHealth;

        _healthImageCollection = new List<Image>(_playerHealthConfigs.HealthCount);

        SpawnHealthImage();
    }

    private void SpawnHealthImage()
    {
        for (int i = 0; i < _playerHealthConfigs.HealthCount; i++)
        {

            _healthImageCollection.Add(Instantiate(_playerHealthConfigs.HealthImage, _healthImagesParent));
        }
    }

    public void ReduceHealth(int reduceImageIndex)
    {
            _healthImageCollection[reduceImageIndex].gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _eventBus.OnHealthCountReduced -= ReduceHealth;
    }
}
