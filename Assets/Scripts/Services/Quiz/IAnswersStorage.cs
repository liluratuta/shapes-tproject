using System.Collections.Generic;

namespace ShapesGame.Services.Quiz
{
    public interface IAnswersStorage
    {
        IEnumerable<string> Answers { get; }
        void Collect();
    }
}