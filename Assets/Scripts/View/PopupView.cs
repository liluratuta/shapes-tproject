using ShapesGame.Services.Pause;
using ShapesGame.Services.Quiz;
using TMPro;
using UnityEngine;

namespace ShapesGame.View
{
    public class PopupView : MonoBehaviour
    {
        public TMP_Text Label;
        public ClickObserver ClickObserver;
        
        private IQuizGameLauncher _quizGameLauncher;
        private IPauseService _pauseService;

        public void Init(IQuizGameLauncher quizGameLauncher, IPauseService pauseService)
        {
            _quizGameLauncher = quizGameLauncher;
            _pauseService = pauseService;
        }
        
        public void SetText(string text) => 
            Label.text = text;

        private void OnEnable()
        {
            ClickObserver.Clicked += OnClicked;
        }

        private void OnDisable()
        {
            ClickObserver.Clicked -= OnClicked;
        }

        private void OnClicked(Vector2 position)
        {
            if (_pauseService.IsPause)
                return;
            
            _quizGameLauncher.Launch();
        }
    }
}