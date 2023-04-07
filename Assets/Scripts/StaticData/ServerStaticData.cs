using UnityEngine;

namespace ShapesGame.StaticData
{
    [CreateAssetMenu(menuName = "Game/StaticData/ServerData", fileName = "ServerData")]
    public class ServerStaticData : ScriptableObject
    {
        public bool IsFakeServer;
        public string ServerPostUrl;
        public string ServerGetUrl;
    }
}