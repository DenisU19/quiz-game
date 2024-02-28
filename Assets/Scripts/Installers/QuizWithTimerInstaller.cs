using UnityEngine;
using Zenject;

public class QuizWithTimerInstaller : MonoInstaller
{
    [SerializeField] private QuizConfigs _quizConfigs;
    [SerializeField] private GameTimerConfigs _gameTimerConfigs;
    [SerializeField] private GameTimerViewDrawer _gameTimerViewDrawer;
    public override void InstallBindings()
    {
        Container.Bind<QuizConfigs>().FromInstance(_quizConfigs).AsSingle();

        Container.Bind<GameTimerViewDrawer>().FromInstance(_gameTimerViewDrawer).AsSingle();

        Container.Bind<GameTimerConfigs>().FromInstance(_gameTimerConfigs).AsSingle();

        Container.BindInterfacesTo<GameTimerManager>().AsSingle();

        Container.BindInterfacesTo<QuizItemSelector>().AsSingle();

    }
}