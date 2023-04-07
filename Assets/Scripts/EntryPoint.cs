using System.Collections.Generic;
using System.Linq;
using ShapesGame.Factories;
using ShapesGame.Quiz.Server;
using ShapesGame.Quiz.States;
using ShapesGame.Services.Asset;
using ShapesGame.Services.Input;
using ShapesGame.Services.Pause;
using ShapesGame.Services.Player;
using ShapesGame.Services.Popup;
using ShapesGame.Services.Quiz;
using ShapesGame.Services.StaticData;
using ShapesGame.Services.Victory;
using ShapesGame.View;
using UnityEngine;

namespace ShapesGame
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private const string AnswerTag = "Answer";
        
        public Camera MainCamera;
        public ClickObserver GameFieldClickObserver;

        private PlayerFactory _playerFactory;
        private IAssetProvider _assetProvider;
        private StaticDataService _staticDataService;
        private IInputService _inputService;
        private PopupService _popupService;
        private IAnswersStorage _answersStorage;
        private IVictoryService _victoryService;
        private IQuizGameLauncher _quizGameLauncher;
        private IUIFactory _uiFactory;
        private PlayerProvider _playerProvider;
        private IQuizServer _quizServer;
        private PauseService _pauseService;

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            _popupService.Prewarm();

            var player = _playerFactory.Create(Vector3.zero);
            _playerProvider.Player = player;
            
            _uiFactory.CreateUIRoot();
        }

        private void OnDestroy()
        {
            _inputService.Dispose();
        }

        private void RegisterServices()
        {
            _pauseService = new PauseService();
            _assetProvider = new AssetProvider();
            _staticDataService = new StaticDataService();
            _staticDataService.Load();

            _inputService = new DesktopInputService(GameFieldClickObserver);

            _uiFactory = new UIFactory(_assetProvider, _staticDataService);

            _answersStorage = new AnswersStorage(CollectAnswers());

            _victoryService = new VictoryService(_uiFactory, _pauseService);
            _playerProvider = new PlayerProvider();

            _quizServer = _staticDataService.ServerData.IsFakeServer
                ? new FakeQuizServer()
                : new QuizServer(coroutineRunner: this, _staticDataService);

            _quizGameLauncher = new QuizGameLauncher(CreateQuizStateMachine());

            _popupService = new PopupService(_assetProvider, _pauseService, _quizGameLauncher);
            _playerFactory = new PlayerFactory(_assetProvider, _staticDataService, _inputService, MainCamera,
                _popupService, _quizGameLauncher, _pauseService);
        }

        private QuizStateMachine CreateQuizStateMachine() => 
            new QuizStateMachine(_playerProvider, _uiFactory, _quizServer, _answersStorage, _victoryService);

        private static IEnumerable<string> CollectAnswers()
        {
            var answerObjects = GameObject.FindGameObjectsWithTag(AnswerTag);
            return answerObjects.Select(x => x.name);
        }
    }
}