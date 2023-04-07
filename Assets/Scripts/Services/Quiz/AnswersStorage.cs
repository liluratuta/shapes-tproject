using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShapesGame.Services.Quiz
{
    public class AnswersStorage : IAnswersStorage
    {
        private const string AnswerTag = "Answer";
        
        public IEnumerable<string> Answers { get; private set; }

        public void Collect()
        {
            var answerObjects = GameObject.FindGameObjectsWithTag(AnswerTag);
            Answers = answerObjects.Select(x => x.name);
        }
    }
}