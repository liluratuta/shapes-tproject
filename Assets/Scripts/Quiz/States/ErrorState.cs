using ShapesGame.Infrastructure.Factories;
using ShapesGame.View.Quiz;

namespace ShapesGame.Quiz.States
{
    public class ErrorState : IPayloadedState<string>
    {
        private readonly IStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        
        private QuizErrorWindow _errorWindow;

        public ErrorState(IStateMachine stateMachine, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
        }

        public void Enter(string errorMessage)
        {
            _errorWindow = _uiFactory.CreateQuizErrorWindow(errorMessage);
            _errorWindow.RetryClicked += OnRetryClicked;
            _errorWindow.CancelClicked += OnCancelClicked;
        }

        public void Exit()
        {
            _errorWindow.RetryClicked -= OnRetryClicked;
            _errorWindow.CancelClicked -= OnCancelClicked;
            _errorWindow.Close();
        }

        private void OnCancelClicked() => 
            _stateMachine.Enter<FinishState>();

        private void OnRetryClicked() => 
            _stateMachine.Enter<CollectRightAnswersState>();
    }
}