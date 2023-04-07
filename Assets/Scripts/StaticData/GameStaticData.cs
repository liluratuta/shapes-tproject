using UnityEngine;

namespace ShapesGame.StaticData
{
    [CreateAssetMenu(menuName = "Game/StaticData/GameData", fileName = "GameData")]
    public class GameStaticData : ScriptableObject
    {
        public string PopupText;
        public Vector3 PopupOffset;
        public LayerMask ShapesLayerMask;
    }
}