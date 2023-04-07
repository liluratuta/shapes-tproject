using System;
using System.Collections.Generic;
using System.Linq;
using ShapesGame.Services.Quiz;

namespace ShapesGame.Quiz
{
    public class QuizGame
    {
        public event Action<int> AnswerSelected;
        public event Action Won;
        
        public IReadOnlyList<Answer> Answers => _answersMap.Values.ToList();
        
        private readonly Dictionary<int, Answer> _answersMap;

        private int _answersNumberToGuess;

        public QuizGame(IAnswersStorage answersStorage, HashSet<string> rightAnswers)
        {
            _answersMap = new Dictionary<int, Answer>();

            var answers = answersStorage.Answers.ToArray();

            for (var id = 0; id < answers.Length; id++)
            {
                var isRight = rightAnswers.Contains(answers[id]);
                
                _answersMap.Add(id, new Answer
                {
                    ID = id,
                    IsSelected = false,
                    Text = answers[id],
                    IsRight = isRight
                });

                if (isRight)
                    _answersNumberToGuess++;
            }
        }

        public void SelectAnswer(int id)
        {
            if (!_answersMap.TryGetValue(id, out var answer))
                return;

            if (answer.IsSelected)
                return;
            
            SetAnswerAsSelected(answer);

            if (CheckIsWon())
                Won?.Invoke();
        }

        private void SetAnswerAsSelected(Answer answer)
        {
            answer.IsSelected = true;
            _answersMap[answer.ID] = answer;

            if (answer.IsRight)
                _answersNumberToGuess--;
            
            AnswerSelected?.Invoke(answer.ID);
        }

        private bool CheckIsWon() => 
            _answersNumberToGuess <= 0;
    }
}