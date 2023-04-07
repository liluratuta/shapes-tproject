using System;
using ShapesGame.Quiz;
using ShapesGame.Services.StaticData;
using TMPro;

namespace ShapesGame.View.Quiz
{
    public class QuizGamePresenter : IDisposable
    {
        private readonly QuizGameWindow _window;
        private readonly QuizGame _game;
        private readonly AnswersTextBuilder _textBuilder;

        public QuizGamePresenter(QuizGameWindow window, QuizGame game, IStaticDataService dataService)
        {
            _window = window;
            _game = game;
            _textBuilder = new AnswersTextBuilder(dataService);

            _game.AnswerSelected += OnAnswerSelected;
            
            _window.SetAnswers(_textBuilder.Build(_game.Answers));
        }

        public void Dispose() => 
            _game.AnswerSelected -= OnAnswerSelected;

        private void OnAnswerSelected(int answerID) => 
            _window.SetAnswers(_textBuilder.Build(_game.Answers));

        public void ClickLink(TMP_LinkInfo linkInfo)
        {
            if (!int.TryParse(linkInfo.GetLinkID(), out var id))
                return;
            
            _game.SelectAnswer(id);
        }
    }
}