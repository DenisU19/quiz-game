using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private EventBus _eventBus;

    public override void InstallBindings()
    {
        Container.Bind<EventBus>().FromInstance(_eventBus);
    }
}