namespace ShapesGame.Services.Pause
{
    public class PauseService : IPauseService
    {
        public bool IsPause { get; private set; }

        public void SetPause(bool isPause) => 
            IsPause = isPause;
    }
}