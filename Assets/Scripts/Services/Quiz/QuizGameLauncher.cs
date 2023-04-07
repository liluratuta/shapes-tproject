using ShapesGame.Quiz.States;

namespace ShapesGame.Services.Quiz
{
    public class QuizGameLauncher : IQuizGameLauncher
    {
        private readonly IStateMachine _stateMachine;

        public QuizGameLauncher(QuizStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Launch() => 
            _stateMachine.Enter<CollectRightAnswersState>();

        public void Finish() => 
            _stateMachine.Enter<FinishState>();
    }
}