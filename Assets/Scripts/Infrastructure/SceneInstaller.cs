using ShapesGame.Infrastructure.Factories;
using ShapesGame.Quiz.Server;
using ShapesGame.Quiz.States;
using ShapesGame.Services.Input;
using ShapesGame.Services.Pause;
using ShapesGame.Services.Player;
using ShapesGame.Services.Popup;
using ShapesGame.Services.Quiz;
using ShapesGame.Services.StaticData;
using ShapesGame.Services.Victory;
using ShapesGame.View;
using UnityEngine;
using Zenject;

namespace ShapesGame.Infrastructure
{
    public class SceneInstaller : MonoInstaller, IInitializable, ICoroutineRunner
    {
        public Camera MainCamera;
        public ClickObserver GameFieldClickObserver;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneInstaller>().FromInstance(this).AsSingle();
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<ClickObserver>().FromInstance(GameFieldClickObserver).AsSingle();
            Container.Bind<IInputService>().To<DesktopInputService>().AsSingle();
            Container.Bind<IQuizServer>().To<QuizServer>().AsSingle();
            Container.Bind<IVictoryService>().To<VictoryService>().AsSingle();
            Container.Bind<IAnswersStorage>().To<AnswersStorage>().AsSingle();
            Container.Bind<IPlayerProvider>().To<PlayerProvider>().AsSingle();
            Container.Bind<QuizStateMachine>().AsSingle();
            Container.Bind<IQuizGameLauncher>().To<QuizGameLauncher>().AsSingle();
            Container.Bind<IPopupService>().To<PopupService>().AsSingle();
            Container.Bind<Camera>().FromInstance(MainCamera).AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IStaticDataService>().Load();
            Container.Resolve<IAnswersStorage>().Collect();
            Container.Resolve<IPopupService>().Prewarm();

            var player = Container.Resolve<IPlayerFactory>().Create(Vector3.zero);
            Container.Resolve<IPlayerProvider>().Player = player;
            
            Container.Resolve<IUIFactory>().CreateUIRoot();
        }

        private void OnDestroy()
        {
            Container.Resolve<IInputService>().Dispose();
        }
    }
}