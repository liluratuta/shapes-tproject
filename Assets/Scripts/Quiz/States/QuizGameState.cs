using ShapesGame.Factories;
using ShapesGame.Services.Quiz;
using ShapesGame.View.Quiz;
using Unity.VisualScripting;

namespace ShapesGame.Quiz.States
{
    public class QuizGameState : IPayloadedState<string[]>
    {
        private readonly IStateMachine _stateMachine;
        private readonly IAnswersStorage _answersStorage;
        private readonly IUIFactory _uiFactory;

        private QuizGame _quizGame;
        private QuizGameWindow _quizWindow;

        public QuizGameState(IStateMachine stateMachine, IAnswersStorage answersStorage, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _answersStorage = answersStorage;
            _uiFactory = uiFactory;
        }

        public void Enter(string[] rightAnswers)
        {
            _quizGame = new QuizGame(_answersStorage, rightAnswers.ToHashSet());
            _quizWindow = _uiFactory.CreateQuizWindow(_quizGame);

            _quizGame.Won += OnWon;
        }

        public void Exit()
        {
            _quizGame.Won -= OnWon;
            _quizGame = null;
            
            _quizWindow.Close();
            _quizWindow = null;
        }

        private void OnWon() => 
            _stateMachine.Enter<VictoryState>();
    }
}