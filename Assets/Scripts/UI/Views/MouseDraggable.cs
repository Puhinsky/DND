using Puhinsky.DND.Core.GameState;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Puhinsky.DND.UI
{
    public class MouseDraggable : MonoBehaviour
    {
        private GameModel _gameModel;

        private void Awake()
        {
            _gameModel = FindAnyObjectByType<GameStateMachine>().Model;
        }

        private void OnMouseDrag()
        {
            if (_gameModel.State.Value == GameStateType.Game)
                SetPosition(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }

        public void SetPosition(Vector3 rawPosition)
        {
            transform.position = new Vector3(rawPosition.x, rawPosition.y, transform.position.z);
        }
    }
}
