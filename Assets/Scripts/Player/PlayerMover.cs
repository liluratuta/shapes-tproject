using ShapesGame.Services.Input;
using ShapesGame.Services.Pause;
using ShapesGame.Services.StaticData;
using UnityEngine;
using Zenject;

namespace ShapesGame.Player
{
    public class PlayerMover : MonoBehaviour
    {
        private const float StopDistance = 0.01f;
        
        private IInputService _inputService;
        private float _speed;
        private Transform _transform;
        private bool _moveToClick;
        private Vector3 _clickPosition;
        private Camera _camera;
        private IPauseService _pauseService;

        [Inject]
        public void Construct(IInputService inputService,
            IStaticDataService staticDataService,
            IPauseService pauseService,
            Camera mainCamera)
        {
            _pauseService = pauseService;
            _inputService = inputService;
            _speed = staticDataService.PlayerData.Speed;
            _transform = transform;
            _camera = mainCamera;
            
            _inputService.GameFieldClicked += OnClicked;
        }

        private void Update()
        {
            if (_pauseService.IsPause)
                return;

            MoveByAxis();

            if (_moveToClick)
            {
                MoveToClick();
            }
        }

        private void OnDestroy() => 
            _inputService.GameFieldClicked -= OnClicked;

        private void OnClicked(Vector2 position)
        {
            if (_pauseService.IsPause)
                return;
            
            _clickPosition = _camera.ScreenToWorldPoint(position);
            _clickPosition.z = 0;
            _moveToClick = true;
        }

        private void MoveByAxis()
        {
            if (_inputService.Axis.sqrMagnitude <= Constants.Epsilon)
                return;

            DisableMoveToClick();

            var movement = (Vector3)_inputService.Axis * _speed * Time.deltaTime;
            _transform.position += movement;

            void DisableMoveToClick()
            {
                if (_moveToClick) _moveToClick = false;
            }
        }

        private void MoveToClick()
        {
            var moveVector = _clickPosition - _transform.position;

            if (moveVector.sqrMagnitude < StopDistance)
            {
                _moveToClick = false;
                return;
            }

            _transform.position += moveVector.normalized * _speed * Time.deltaTime;
        }
    }
}
