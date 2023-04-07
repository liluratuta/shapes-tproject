using ShapesGame.Quiz.Server;
using UnityEngine;

namespace ShapesGame.Quiz.States
{
    public class SendAnswersState : IPayloadedState<string[]>
    {
        private readonly IStateMachine _stateMachine;
        private readonly IQuizServer _quizServer;

        public SendAnswersState(IStateMachine stateMachine, IQuizServer quizServer)
        {
            _stateMachine = stateMachine;
            _quizServer = quizServer;
        }

        public void Enter(string[] rightAnswers)
        {
            var answersData = new AnswersData
            {
                Answers = rightAnswers
            };

            var json = JsonUtility.ToJson(answersData);
            
            _quizServer.SendRightAnswers(json, OnSendAnswersDone);
        }

        public void Exit()
        {
            _quizServer.StopPreviousRequest();
        }

        private void OnSendAnswersDone(Response response)
        {
            if (!response.IsSuccess)
            {
                _stateMachine.Enter<ErrorState, string>(response.ErrorMessage);
                return;
            }

            // var responseData = JsonUtility.FromJson<PostResponseData>(response.Result);
            
            _stateMachine.Enter<LoadAnswersState, string>(response.Result);
        }
    }
}