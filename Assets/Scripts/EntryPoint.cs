using ShapesGame.Infrastructure.Factories;
using ShapesGame.Services.Input;
using ShapesGame.Services.Player;
using ShapesGame.Services.Popup;
using ShapesGame.Services.Quiz;
using ShapesGame.Services.StaticData;
using ShapesGame.View;
using UnityEngine;
using Zenject;

namespace ShapesGame
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        public ClickObserver GameFieldClickObserver;

        private IPlayerFactory _playerFactory;
        private IStaticDataService _staticDataService;
        private IInputService _inputService;
        private IPopupService _popupService;
        private IAnswersStorage _answersStorage;
        private IUIFactory _uiFactory;
        private IPlayerProvider _playerProvider;

        [Inject]
        public void Init(IPlayerFactory playerFactory,
            IStaticDataService staticDataService,
            IInputService inputService,
            IPopupService popupService,
            IAnswersStorage answersStorage,
            IUIFactory uiFactory,
            IPlayerProvider playerProvider)
        {
            _playerFactory = playerFactory;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _popupService = popupService;
            _answersStorage = answersStorage;
            _uiFactory = uiFactory;
            _playerProvider = playerProvider;
        }
        
        private void Start()
        {
            _staticDataService.Load();
            _answersStorage.Collect();
            _popupService.Prewarm();

            var player = _playerFactory.Create(Vector3.zero);
            _playerProvider.Player = player;
            
            _uiFactory.CreateUIRoot();
        }

        private void OnDestroy()
        {
            _inputService.Dispose();
        }
    }
}