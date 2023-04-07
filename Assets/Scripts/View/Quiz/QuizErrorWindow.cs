using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShapesGame.View.Quiz
{
    public class QuizErrorWindow : MonoBehaviour
    {
        public event Action RetryClicked;
        public event Action CancelClicked;

        public Button RetryButton;
        public Button CancelButton;
        public TMP_Text Label;

        public void SetMessage(string message) => 
            Label.text = message;

        public void Close() => 
            Destroy(gameObject);

        private void OnEnable()
        {
            RetryButton.onClick.AddListener(OnRetryClicked);
            CancelButton.onClick.AddListener(OnCancelClicked);
        }

        private void OnDisable()
        {
            RetryButton.onClick.RemoveListener(OnRetryClicked);
            CancelButton.onClick.RemoveListener(OnCancelClicked);
        }

        private void OnRetryClicked()
        {
            SetInteractableButtons(false);
            RetryClicked?.Invoke();
        }

        private void OnCancelClicked()
        {
            SetInteractableButtons(false);
            CancelClicked?.Invoke();
        }

        private void SetInteractableButtons(bool interactable)
        {
            RetryButton.interactable = interactable;
            CancelButton.interactable = interactable;
        }
    }
}