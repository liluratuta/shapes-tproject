using ShapesGame.Services.Asset;
using ShapesGame.Services.Pause;
using ShapesGame.Services.Quiz;
using ShapesGame.View;
using UnityEngine;

namespace ShapesGame.Services.Popup
{
    public class PopupService : IPopupService
    {
        private readonly IAssetProvider _assets;
        private readonly IPauseService _pauseService;
        private readonly IQuizGameLauncher _quizGameLauncher;

        private PopupView _popup;
        
        public PopupService(IAssetProvider assets, IPauseService pauseService, IQuizGameLauncher quizGameLauncher)
        {
            _assets = assets;
            _pauseService = pauseService;
            _quizGameLauncher = quizGameLauncher;
        }

        public void Prewarm()
        {
            var prefab = _assets.Get(AssetPath.Popup);
            var popup = Object.Instantiate(prefab);
            popup.SetActive(false);
            _popup = popup.GetComponent<PopupView>();
            _popup.Init(_quizGameLauncher, _pauseService);
        }

        public void Show(string text, Vector3 position)
        {
            _popup.transform.position = position;
            _popup.gameObject.SetActive(true);
            _popup.SetText(text);
        }

        public void Hide() =>
            _popup.gameObject.SetActive(false);
    }
}