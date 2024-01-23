using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Puhinsky.DND.Core.GameState
{
    [RequireComponent(typeof(PlayerInput))]
    public class GameStateMachine : MonoBehaviour
    {
        public GameModel Model { get; private set; } = new();

        private const string _toggleGameAction = "Game Toggle";

        private void Awake()
        {
            var input = GetComponent<PlayerInput>();
            input.actions.FindAction(_toggleGameAction).performed += OnToggleGame;
        }

        private void OnToggleGame(CallbackContext context)
        {
            switch (Model.State.Value)
            {
                case GameStateType.Game:
                    Model.State.Value = GameStateType.Setup;
                    break;
                case GameStateType.Setup:
                    Model.State.Value = GameStateType.Game;
                    break;
            }
        }
    }
}
