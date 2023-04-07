using ShapesGame.Factories;
using ShapesGame.Services.Pause;

namespace ShapesGame.Services.Victory
{
    public class VictoryService : IVictoryService
    {
        private readonly IUIFactory _uiFactory;
        private readonly IPauseService _pauseService;
        
        public VictoryService(IUIFactory uiFactory, IPauseService pauseService)
        {
            _uiFactory = uiFactory;
            _pauseService = pauseService;
        }

        public void Win()
        {
            _pauseService.SetPause(true);
            _uiFactory.CreateVictoryWindow();
        }
    }
}