using Puhinsky.DND.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class PlayerSelectionController : VisualElement
    {
        private readonly Foldout _foldout = new() { value = false };
        private readonly IntegerField _powerField = new() { label = PlayerLabels.Power, enabledSelf = false };

        private const string _playerSelectCssClass = "player-select";

        public PlayerSelectionController()
        {
            var handler = Object.FindAnyObjectByType<PlayerSelectionHandler>();
            handler.PlayerSelected += OnPlayerSelected;
            handler.PlayerDeselected += OnPlayerDeselected;

            _foldout.Add(_powerField);
            Add(_foldout);
            AddToClassList(_playerSelectCssClass);
        }

        private void OnPlayerSelected(PlayerModel player)
        {
            _foldout.text = player.Name.Value;
            _foldout.style.color = player.Color.Value;
        }

        private void OnPlayerDeselected(PlayerModel player)
        {
            _foldout.style.color = Color.gray;
            _foldout.text = string.Empty;
        }
    }
}
