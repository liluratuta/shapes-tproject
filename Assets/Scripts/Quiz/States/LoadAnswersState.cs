using ShapesGame.Quiz.Server;
using UnityEngine;

namespace ShapesGame.Quiz.States
{
    public class LoadAnswersState : IPayloadedState<string>
    {
        private readonly IStateMachine _stateMachine;
        private readonly IQuizServer _quizServer;

        public LoadAnswersState(IStateMachine stateMachine, IQuizServer quizServer)
        {
            _stateMachine = stateMachine;
            _quizServer = quizServer;
        }

        public void Enter(string recordID)
        {
            _quizServer.LoadRightAnswers(recordID, OnLoadAnswersDone);
        }

        public void Exit()
        {
            _quizServer.StopPreviousRequest();
        }

        private void OnLoadAnswersDone(Response response)
        {
            if (!response.IsSuccess)
            {
                _stateMachine.Enter<ErrorState, string>(response.ErrorMessage);
                return;
            }

            var answersData = JsonUtility.FromJson<AnswersData>(response.Result);
            
            _stateMachine.Enter<QuizGameState, string[]>(answersData.Answers);
        }
    }
}