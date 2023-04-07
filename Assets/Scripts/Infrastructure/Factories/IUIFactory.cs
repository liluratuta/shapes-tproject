using ShapesGame.Quiz;
using ShapesGame.View;
using ShapesGame.View.Quiz;

namespace ShapesGame.Infrastructure.Factories
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        QuizGameWindow CreateQuizWindow(QuizGame quizGame);
        QuizErrorWindow CreateQuizErrorWindow(string message);
        VictoryWindow CreateVictoryWindow();
    }
}