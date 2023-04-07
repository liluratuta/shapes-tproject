using ShapesGame.Quiz;
using ShapesGame.Services.Asset;
using ShapesGame.View;
using ShapesGame.View.Quiz;
using UnityEngine;
using Zenject;

namespace ShapesGame.Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly DiContainer _diContainer;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, DiContainer diContainer)
        {
            _assets = assets;
            _diContainer = diContainer;
        }

        public void CreateUIRoot()
        {
            var prefab = _assets.Get(AssetPath.UIRoot);
            _uiRoot = _diContainer.InstantiatePrefab(prefab).transform;
        }

        public QuizGameWindow CreateQuizWindow(QuizGame quizGame)
        {
            var prefab = _assets.Get(AssetPath.QuizWindow);
            var window = _diContainer.InstantiatePrefabForComponent<QuizGameWindow>(prefab, _uiRoot);
            window.SetGame(quizGame);
            return window;
        }

        public QuizErrorWindow CreateQuizErrorWindow(string message)
        {
            var prefab = _assets.Get(AssetPath.QuizErrorWindow);
            var window = _diContainer.InstantiatePrefabForComponent<QuizErrorWindow>(prefab, _uiRoot);
            window.SetMessage(message);
            return window;
        }

        public VictoryWindow CreateVictoryWindow()
        {
            var prefab = _assets.Get(AssetPath.VictoryWindow);
            return _diContainer.InstantiatePrefabForComponent<VictoryWindow>(prefab, _uiRoot);
        }
    }
}