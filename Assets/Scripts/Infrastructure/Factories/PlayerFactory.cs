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
            return _diContainer.InstantiatePrefab(prefab, position, Quaternion.identity, null);
        }
    }
}