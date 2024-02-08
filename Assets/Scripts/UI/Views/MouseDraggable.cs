using Puhinsky.DND.Core.GameState;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Puhinsky.DND.UI
{
    public class MouseDraggable : MonoBehaviour
    {
        private GameModel _gameModel;
        private Vector3 _deltaPosition;

        private void Awake()
        {
            _gameModel = FindAnyObjectByType<GameStateMachine>().Model;
        }

        private void OnMouseDown()
        {
            _deltaPosition = transform.position - GetMousePosition();
        }

        private void OnMouseDrag()
        {
            if (_gameModel.State.Value == GameStateType.Game)
                SetPosition(GetMousePosition() + _deltaPosition);
        }

        public void SetPosition(Vector3 rawPosition)
        {
            transform.position = new Vector3(rawPosition.x, rawPosition.y, transform.position.z);
        }

        private Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}
