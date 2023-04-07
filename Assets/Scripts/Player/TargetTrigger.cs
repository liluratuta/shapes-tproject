using ShapesGame.Services.Input;
using ShapesGame.Services.Pause;
using ShapesGame.Services.Popup;
using ShapesGame.Services.Quiz;
using ShapesGame.StaticData;
using ShapesGame.Target;
using UnityEngine;

namespace ShapesGame.Player
{
    public class TargetTrigger : MonoBehaviour
    {
        private IPopupService _popupService;
        private string _popupText;
        private Vector3 _popupOffset;
        private bool _isStayOnTarget;
        private IInputService _inputService;
        private IQuizGameLauncher _quizGameLauncher;
        private IPauseService _pauseService;

        public void Init(IPopupService popupService,
            GameStaticData gameStaticData,
            IInputService inputService,
            IQuizGameLauncher quizGameLauncher,
            IPauseService pauseService)
        {
            _popupService = popupService;
            _popupText = gameStaticData.PopupText;
            _popupOffset = gameStaticData.PopupOffset;
            _inputService = inputService;
            _quizGameLauncher = quizGameLauncher;
            _pauseService = pauseService;
        }

        private void Update()
        {
            if (_pauseService.IsPause)
                return;
            
            if (_isStayOnTarget && _inputService.IsSelected)
            {
                _quizGameLauncher.Launch();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<ITarget>(out var target)) 
                return;
            
            _popupService.Show(_popupText, target.Position + _popupOffset);
            _isStayOnTarget = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent<ITarget>(out var target))
                return;

            _popupService.Hide();
            _isStayOnTarget = false;

            _quizGameLauncher.Finish();
        }
    }
}