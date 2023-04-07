namespace ShapesGame.Services.Pause
{
    public interface IPauseService
    {
        bool IsPause { get; }
        void SetPause(bool isPause);
    }
}