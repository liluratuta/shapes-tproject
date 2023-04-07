using ShapesGame.Player;
using ShapesGame.Services.Asset;
using ShapesGame.Services.Input;
using ShapesGame.Services.Pause;
using ShapesGame.Services.Popup;
using ShapesGame.Services.Quiz;
using ShapesGame.Services.StaticData;
using UnityEngine;

namespace ShapesGame.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly Camera _camera;
        private readonly IPopupService _popupService;
        private readonly IQuizGameLauncher _quizGameLauncher;
        private readonly IPauseService _pauseService;

        public PlayerFactory(IAssetProvider assets,
            IStaticDataService staticDataService,
            IInputService inputService,
            Camera camera, 
            IPopupService popupService,
            IQuizGameLauncher quizGameLauncher,
            IPauseService pauseService)
        {
            _assets = assets;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _camera = camera;
            _popupService = popupService;
            _quizGameLauncher = quizGameLauncher;
            _pauseService = pauseService;
        }

        public GameObject Create(Vector3 position)
        {
            var prefab = _assets.Get(AssetPath.Player);
            var player = Object.Instantiate(prefab, position, Quaternion.identity);

            var playerMover = player.GetComponent<PlayerMover>();
            playerMover.Init(_inputService, _staticDataService.PlayerData, _pauseService);
            playerMover.SetCamera(_camera);

            var targetTrigger = player.GetComponentInChildren<TargetTrigger>();
            targetTrigger.Init(_popupService, _staticDataService.GameData, _inputService, _quizGameLauncher, _pauseService);

            var collector = player.GetComponent<ShapesCollector>();
            collector.Init(_staticDataService);
            
            return player;
        }
    }
}