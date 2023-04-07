using System;
using System.Collections;
using System.Text;
using ShapesGame.Services.StaticData;
using ShapesGame.StaticData;
using UnityEngine;
using UnityEngine.Networking;

namespace ShapesGame.Quiz.Server
{
    public class QuizServer : IQuizServer
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ServerStaticData _serverData;

        private Coroutine _currentCoroutine;
        
        public QuizServer(ICoroutineRunner coroutineRunner, IStaticDataService staticDataService)
        {
            _coroutineRunner = coroutineRunner;
            _serverData = staticDataService.ServerData;
        }

        public void SendRightAnswers(string answers, Action<Response> onDone)
        {
            StopPreviousRequest();
            
            _currentCoroutine = _coroutineRunner
                .StartCoroutine(SendJsonToServer(_serverData.ServerPostUrl, answers, onDone));
        }

        public void LoadRightAnswers(string recordID, Action<Response> onDone)
        {
            StopPreviousRequest();
            
            _currentCoroutine = _coroutineRunner
                .StartCoroutine(LoadJsonFromServer($"{_serverData.ServerGetUrl}?record_id={recordID}", onDone));
        }

        public void StopPreviousRequest()
        {
            if (_currentCoroutine == null) 
                return;
            
            _coroutineRunner.StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        private IEnumerator SendJsonToServer(string url, string json, Action<Response> response)
        {
            var formData = new WWWForm();

            using var request = UnityWebRequest.Post(url, formData);
            var postBytes = Encoding.UTF8.GetBytes(json);

            var uploadHandler = new UploadHandlerRaw(postBytes);

            request.uploadHandler = uploadHandler;
            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
            
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                response.Invoke(new Response(false, errorMessage: request.error));
                yield break;
            }
            
            response.Invoke(new Response(true, result: request.downloadHandler.text));
        }

        private IEnumerator LoadJsonFromServer(string url, Action<Response> response)
        {
            using var request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                response.Invoke(new Response(false, errorMessage: request.error));
                yield break;
            }

            response.Invoke(new Response(true, result: request.downloadHandler.text));
        }
    }
}