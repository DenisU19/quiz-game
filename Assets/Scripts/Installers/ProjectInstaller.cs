using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        DeclareSignals();

        Container.BindInterfacesTo<PlayerAnswerVerifier>().AsSingle();
    }

    public void DeclareSignals()
    {
        Container.DeclareSignal<QuizItemSelectedSignal>();
        Container.DeclareSignal<PlayerAnswerSelectedSignal>();
        Container.DeclareSignal<PlayerAnswerCorrectSignal>();
        Container.DeclareSignal<PlayerAnswerWrongSignal>();
        Container.DeclareSignal<DetermineCorrectAnswerSignal>();
        Container.DeclareSignal<SelectNewQuizItemSignal>();
        Container.DeclareSignal<PunishPlayerSignal>();
        Container.DeclareSignal<FreezeGameSignal>();
        Container.DeclareSignal<GameOverSignal>();
    }
}