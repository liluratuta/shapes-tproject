using System.Collections.Generic;
using System.Text;
using ShapesGame.Services.StaticData;
using ShapesGame.StaticData;

namespace ShapesGame.Quiz
{
    public class AnswersTextBuilder
    {
        private readonly QuizStaticData _quizData;
        
        public AnswersTextBuilder(IStaticDataService dataService)
        {
            _quizData = dataService.QuizData;
        }

        public string Build(IReadOnlyList<Answer> answers)
        {
            var stringBuilder = new StringBuilder();

            foreach (var answer in answers)
            {
                if (answer.IsSelected) 
                    stringBuilder.Append($"<color={(answer.IsRight ? _quizData.RightColor : _quizData.WrongColor)}>");

                stringBuilder.Append($"<link=\"{answer.ID}\">{answer.Text}</link>");

                if (answer.IsSelected)
                    stringBuilder.Append("</color>");

                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}