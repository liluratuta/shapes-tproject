using ShapesGame.StaticData;
using UnityEngine;

namespace ShapesGame.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string PlayerDataPath = "StaticData/PlayerData";
        private const string GameDataPath = "StaticData/GameData";
        private const string ServerDataPath = "StaticData/ServerData";
        private const string QuizDataPath = "StaticData/QuizData";
        
        public PlayerStaticData PlayerData { get; private set; }
        public GameStaticData GameData { get; private set; }
        public ServerStaticData ServerData { get; private set; }
        public QuizStaticData QuizData { get; private set; }

        public void Load()
        {
            PlayerData = Resources.Load<PlayerStaticData>(PlayerDataPath);
            GameData = Resources.Load<GameStaticData>(GameDataPath);
            ServerData = Resources.Load<ServerStaticData>(ServerDataPath);
            QuizData = Resources.Load<QuizStaticData>(QuizDataPath);
        }

    }
}