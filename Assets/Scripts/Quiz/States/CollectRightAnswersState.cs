using ShapesGame.Player;
using ShapesGame.Services.Player;

namespace ShapesGame.Quiz.States
{
    public class CollectRightAnswersState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IPlayerProvider _playerProvider;

        public CollectRightAnswersState(IStateMachine stateMachine, IPlayerProvider playerProvider)
        {
            _stateMachine = stateMachine;
            _playerProvider = playerProvider;
        }

        public void Enter()
        {
            if (!_playerProvider.Player.TryGetComponent<ShapesCollector>(out var shapesCollector))
            {
                _stateMachine.Enter<FinishState>();
                return;
            }
            var answers = shapesCollector.GetShapeNames();
            _stateMachine.Enter<SendAnswersState, string[]>(answers);
        }

        public void Exit()
        {
        }
    }
}