using Puhinsky.DND.Core.GameState;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    [RequireComponent(typeof(GameStateMachine))]
    [RequireComponent(typeof(UIDocument))]
    public class SetupGameMenu : MonoBehaviour
    {
        private VisualElement _setupContainer;
        private VisualElement _gameContainer;

        private const string _setupContainerCssClass = "setup-container";
        private const string _gameContainerCssClass = "game-container";

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _gameContainer = new();
            _gameContainer.style.flexDirection = FlexDirection.Row;
            _gameContainer.style.flexGrow = 1;
            _gameContainer.AddToClassList(_gameContainerCssClass);

            _setupContainer = new();
            _setupContainer.style.flexDirection = FlexDirection.Row;
            _setupContainer.style.flexGrow = 1;
            _setupContainer.AddToClassList(_setupContainerCssClass);

            var playersList = new PlayerListController("Персонажи");
            _setupContainer.Add(playersList);

            var mapSetupController = new MapSetupController();
            _setupContainer.Add(mapSetupController);

            var bottomPanelController = new BottomPanelController();
            _gameContainer.Add(bottomPanelController);

            root.Add(_gameContainer);
            root.Add(_setupContainer);

            var gameState = GetComponent<GameStateMachine>().Model.State;
            gameState.Changed += OnGameStateChanged;
            OnGameStateChanged(gameState.Value);
        }

        private void OnGameStateChanged(GameStateType state)
        {
            switch (state)
            {
                case GameStateType.Game:
                    _gameContainer.style.display = DisplayStyle.Flex;
                    _setupContainer.style.display = DisplayStyle.None;
                    break;
                case GameStateType.Setup:
                    _gameContainer.style.display = DisplayStyle.None;
                    _setupContainer.style.display = DisplayStyle.Flex;
                    break;
            }
        }
    }
}