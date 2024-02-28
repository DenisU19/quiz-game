using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHealthViewDrawer : MonoBehaviour
{
    [SerializeField] private Transform _healthImagesParent;

    private PlayerHealthConfigs _playerHealthConfigs;
    private List<Image> _healthImageCollection;

    [Inject]
    private void Construct(PlayerHealthConfigs playerHealthConfigs)
    {
        _playerHealthConfigs = playerHealthConfigs;
    }

    private void Awake()
    {
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

    public void DrawLostHeath(int healthCount)
    {
          _healthImageCollection[healthCount].gameObject.SetActive(false);
    }
}
