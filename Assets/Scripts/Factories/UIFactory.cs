using ShapesGame.Quiz;
using ShapesGame.Services.Asset;
using ShapesGame.Services.StaticData;
using ShapesGame.View;
using ShapesGame.View.Quiz;
using UnityEngine;

namespace ShapesGame.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _dataService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService dataService)
        {
            _assets = assets;
            _dataService = dataService;
        }

        public void CreateUIRoot()
        {
            var prefab = _assets.Get(AssetPath.UIRoot);
            _uiRoot = Object.Instantiate(prefab).transform;
        }

        public QuizGameWindow CreateQuizWindow(QuizGame quizGame)
        {
            var prefab = _assets.Get(AssetPath.QuizWindow);
            var window = Object.Instantiate(prefab, _uiRoot);

            var quizView = window.GetComponentInChildren<QuizGameWindow>();
            quizView.Init(quizGame, _dataService);
            
            return quizView;
        }

        public QuizErrorWindow CreateQuizErrorWindow(string message)
        {
            var prefab = _assets.Get(AssetPath.QuizErrorWindow);
            var window = Object.Instantiate(prefab, _uiRoot);

            var view = window.GetComponent<QuizErrorWindow>();
            view.SetMessage(message);

            return view;
        }

        public VictoryWindow CreateVictoryWindow()
        {
            var prefab = _assets.Get(AssetPath.VictoryWindow);
            var window = Object.Instantiate(prefab, _uiRoot);
            return window.GetComponent<VictoryWindow>();
        }
    }
}