using Puhinsky.DND.Core.GameState;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Puhinsky.DND.Core
{
    [RequireComponent(typeof(GameStateMachine))]
    [RequireComponent(typeof(PlayerInput))]
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private float _defaultZoom = 5f;
        [SerializeField] private float _minZoom = 1f;
        [SerializeField] private float _maxZoom = 10f;
        [SerializeField][Range(0.1f, 1f)] private float _zoomSensivity = 0.5f;
        [SerializeField][Range(0.0001f, 0.05f)] private float _moveSensivity = 0.003f;
        [SerializeField] AnimationCurve _sensivityMap = AnimationCurve.Constant(0f, 1f, 1f);

        private const string _mouseScrollingAction = "Zoom";
        private const string _moveMouseAction = "Camera Move";
        private const string _rightMouseAction = "Right Mouse";
        private const string _resetCameraAction = "Reset Camera";

        private InputAction _rightMouse;
        private GameModel _gameModel;

        private void Awake()
        {
            _gameModel = GetComponent<GameStateMachine>().Model;

            var input = GetComponent<PlayerInput>();
            input.actions.FindAction(_mouseScrollingAction).performed += OnZoom;
            input.actions.FindAction(_moveMouseAction).performed += OnCameraMove;
            input.actions.FindAction(_resetCameraAction).performed += OnResetCamera;
            _rightMouse = input.actions.FindAction(_rightMouseAction);

            ResetCamera();
        }

        private void OnZoom(CallbackContext context)
        {
            if (_gameModel.State.Value == GameStateType.Game)
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + context.ReadValue<Vector2>().y * _zoomSensivity * _sensivityMap.Evaluate((Camera.main.orthographicSize - _minZoom) / (_maxZoom - _minZoom)), _minZoom, _maxZoom);
        }

        private void OnCameraMove(CallbackContext context)
        {
            if (_gameModel.State.Value == GameStateType.Game)
            {
                if (_rightMouse.ReadValue<float>() > 0)
                {
                    Camera.main.transform.Translate(_moveSensivity * Camera.main.orthographicSize * context.ReadValue<Vector2>());
                }
            }
        }

        private void OnResetCamera(CallbackContext context)
        {
            if (_gameModel.State.Value == GameStateType.Game)
            {
                ResetCamera();
            }
        }

        private void ResetCamera()
        {
            Camera.main.orthographicSize = _defaultZoom;
            Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
        }
    }
}
