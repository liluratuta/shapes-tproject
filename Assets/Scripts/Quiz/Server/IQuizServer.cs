using System;

namespace ShapesGame.Quiz.Server
{
    public interface IQuizServer
    {
        void SendRightAnswers(string answers, Action<Response> onDone);
        void LoadRightAnswers(string recordID, Action<Response> onDone);
        void StopPreviousRequest();
    }
}