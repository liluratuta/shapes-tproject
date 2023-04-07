using System;
using ShapesGame.View;
using UnityEngine;

namespace ShapesGame.Services.Input
{
    public class DesktopInputService : IInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const KeyCode SelectKeyCode = KeyCode.Space;

        public event Action<Vector2> GameFieldClicked;

        public Vector2 Axis =>
            new Vector2(UnityEngine.Input.GetAxisRaw(HorizontalAxis), UnityEngine.Input.GetAxisRaw(VerticalAxis)).normalized;
        public bool IsSelected => UnityEngine.Input.GetKeyUp(SelectKeyCode);

        private readonly ClickObserver _gameField;

        public DesktopInputService(ClickObserver gameField)
        {
            _gameField = gameField;
            _gameField.Clicked += OnClicked;
        }

        private void OnClicked(Vector2 position) => 
            GameFieldClicked?.Invoke(position);

        public void Dispose() => 
            _gameField.Clicked -= OnClicked;
    }
}
