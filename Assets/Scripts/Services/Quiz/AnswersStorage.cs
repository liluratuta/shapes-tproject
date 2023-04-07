using System.Collections.Generic;

namespace ShapesGame.Services.Quiz
{
    public class AnswersStorage : IAnswersStorage
    {
        public IEnumerable<string> Answers { get; private set; }

        public AnswersStorage(IEnumerable<string> answers)
        {
            Answers = answers;
        }
    }
}