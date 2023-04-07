using UnityEngine;

namespace ShapesGame.StaticData
{
    [CreateAssetMenu(menuName = "Game/StaticData/PlayerData", fileName = "PlayerData")]
    public class PlayerStaticData : ScriptableObject
    {
        public float Speed;
        public Vector2 OverlapBoxSize;
    }
}