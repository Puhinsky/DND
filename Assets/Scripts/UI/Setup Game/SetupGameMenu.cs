using Puhinsky.DND.Core.GameState;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    [RequireComponent(typeof(GameStateMachine))]
    [RequireComponent(typeof(UIDocument))]
    public class SetupGameMenu : MonoBehaviour
    {
        private VisualElement _root;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            var gameState = GetComponent<GameStateMachine>().Model.State;
            gameState.Changed += OnGameStateChanged;
            OnGameStateChanged(gameState.Value);
        }

        private void Start()
        {
            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            container.style.flexGrow = 1;
            container.AddToClassList("root");

            var playersList = new PlayerListController("Игроки");
            container.Add(playersList);

            var mapSetupController = new MapSetupController();
            container.Add(mapSetupController);

            _root.Add(container);
        }

        private void OnGameStateChanged(GameStateType state)
        {
            switch (state)
            {
                case GameStateType.Game:
                    _root.style.display = DisplayStyle.None;
                    break;
                case GameStateType.Setup:
                    _root.style.display = DisplayStyle.Flex;
                    break;
            }
        }
    }
}