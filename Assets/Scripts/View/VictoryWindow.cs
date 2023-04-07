using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShapesGame.View
{
    public class VictoryWindow : MonoBehaviour
    {
        private const string MainScene = "Main";
        
        public Button StartAgainButton;

        private void OnEnable()
        {
            StartAgainButton.onClick.AddListener(StartGameAgain);
        }

        private void OnDisable()
        {
            StartAgainButton.onClick.RemoveListener(StartGameAgain);
        }

        private void StartGameAgain() => 
            SceneManager.LoadScene(MainScene);
    }
}