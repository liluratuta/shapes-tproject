using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShapesGame.Quiz.Server
{
    public class FakeQuizServer : IQuizServer
    {
        private readonly Dictionary<string, string> _records = new Dictionary<string, string>();

        private int _currentID; 
        
        public void SendRightAnswers(string answers, Action<Response> onDone)
        {
            var id = _currentID.ToString();
            _records.Add(id, answers);
            var result = JsonUtility.ToJson(new PostResponseData
            {
                record_id = id
            });
            _currentID++;
            onDone.Invoke(new Response(true, result: result));
        }

        public void LoadRightAnswers(string recordID, Action<Response> onDone)
        {
            if (_records.TryGetValue(recordID, out var answers))
            {
                onDone.Invoke(new Response(true, result: answers));
                return;
            }
            
            onDone.Invoke(new Response(false, errorMessage: $"recordID:{recordID} not found"));
        }

        public void StopPreviousRequest()
        {
        }
    }
}