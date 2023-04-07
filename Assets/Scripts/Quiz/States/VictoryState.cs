using ShapesGame.Services.Victory;

namespace ShapesGame.Quiz.States
{
    public class VictoryState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IVictoryService _victoryService;

        public VictoryState(IStateMachine stateMachine, IVictoryService victoryService)
        {
            _stateMachine = stateMachine;
            _victoryService = victoryService;
        }

        public void Enter()
        {
            _victoryService.Win();
            _stateMachine.Enter<FinishState>();
        }

        public void Exit()
        {
        }
    }
}