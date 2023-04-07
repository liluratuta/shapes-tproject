using ShapesGame.StaticData;

namespace ShapesGame.Services.StaticData
{
    public interface IStaticDataService
    {
        PlayerStaticData PlayerData { get; }
        GameStaticData GameData { get; }
        ServerStaticData ServerData { get; }
        QuizStaticData QuizData { get; }
    }
}