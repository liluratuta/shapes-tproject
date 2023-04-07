using ShapesGame.Quiz;
using ShapesGame.Services.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ShapesGame.View.Quiz
{
    public class QuizGameWindow : MonoBehaviour, IPointerClickHandler
    {
        public TMP_Text Label;

        private QuizGamePresenter _presenter;
        private LinkClickHandler _linkClickHandler;
        private IStaticDataService _staticDataService;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _linkClickHandler = new LinkClickHandler(Label);
        }

        public void SetGame(QuizGame quizGame)
        {
            _presenter = new QuizGamePresenter(this, quizGame, _staticDataService);
        }

        public void SetAnswers(string answers) => 
            Label.text = answers;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_linkClickHandler.TryHandle(eventData.position, out var linkInfo))
                return;

            _presenter.ClickLink(linkInfo);
        }

        public void Close() => 
            Destroy(gameObject);

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}