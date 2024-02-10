using UnityEngine;
using Zenject;

public class QuizWithHealthInstaller : MonoInstaller
{
    [SerializeField] private QuizConfigs _quizConfigs;
    [SerializeField] private PlayerHealthConfigs _playerHealthConfigs;
    public override void InstallBindings()
    {
        Container.Bind<QuizConfigs>().FromInstance(_quizConfigs).AsSingle();

        Container.Bind<PlayerHealthConfigs>().FromInstance(_playerHealthConfigs).AsSingle();

        Container.BindInterfacesTo<PlayerHealthCounter>().AsSingle();

        Container.BindInterfacesTo<PlayerAnswerVerifier>().AsSingle();

        Container.BindInterfacesTo<QuizItemSelector>().AsSingle();

    }
}