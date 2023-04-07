using ShapesGame.Services.Asset;
using UnityEngine;
using Zenject;

namespace ShapesGame.Infrastructure.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProvider _assets;
        private readonly DiContainer _diContainer;

        public PlayerFactory(IAssetProvider assets, DiContainer diContainer)
        {
            _assets = assets;
            _diContainer = diContainer;
        }

        public GameObject Create(Vector3 position)
        {
            var prefab = _assets.Get(AssetPath.Player);
            var player = _diContainer.InstantiatePrefab(prefab, position, Quaternion.identity, null);

            // var playerMover = player.GetComponent<PlayerMover>();
            // playerMover.Init(_inputService, _staticDataService.PlayerData, _pauseService);
            // playerMover.SetCamera(_camera);
            //
            // var targetTrigger = player.GetComponentInChildren<TargetTrigger>();
            // targetTrigger.Init(_popupService, _staticDataService.GameData, _inputService, _quizGameLauncher, _pauseService);
            //
            // var collector = player.GetComponent<ShapesCollector>();
            // collector.Init(_staticDataService);
            
            return player;
        }
    }
}