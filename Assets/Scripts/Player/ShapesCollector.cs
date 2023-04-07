using System.Linq;
using ShapesGame.Services.StaticData;
using ShapesGame.StaticData;
using UnityEngine;

namespace ShapesGame.Player
{
    public class ShapesCollector : MonoBehaviour
    {
        public LayerMask ShapesLayerMask;
        
        private PlayerStaticData _playerData;
        private GameStaticData _gameData;

        public void Init(IStaticDataService dataService)
        {
            _playerData = dataService.PlayerData;
            _gameData = dataService.GameData;
        }
        
        public string[] GetShapeNames()
        {
            var colliders = Physics2D.OverlapBoxAll(transform.position,
                _playerData.OverlapBoxSize, 0, _gameData.ShapesLayerMask);
            return colliders.Select(x => x.gameObject.name).ToArray();
        }
    }
}