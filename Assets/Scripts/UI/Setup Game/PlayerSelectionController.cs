using Puhinsky.DND.Models;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class PlayerSelectionController : VisualElement
    {
        private readonly Foldout _foldout = new() { value = false };
        private readonly IntegerField _power = new() { label = PlayerLabels.Power, isReadOnly = true };
        private readonly IntegerField _agility = new() { label = PlayerLabels.Agility, isReadOnly = true };
        private readonly IntegerField _intelligence = new() { label = PlayerLabels.Intelligence, isReadOnly = true };

        private const string _playerSelectCssClass = "player-select";

        public PlayerSelectionController()
        {
            var handler = Object.FindAnyObjectByType<PlayerSelectionHandler>();
            handler.PlayerSelected += OnPlayerSelected;
            handler.PlayerDeselected += OnPlayerDeselected;

            _foldout.Add(_power);
            _foldout.Add(_agility);
            _foldout.Add(_intelligence);
            Add(_foldout);
            AddToClassList(_playerSelectCssClass);
        }

        private void OnPlayerSelected(PlayerModel player)
        {
            player.Name.BindView(_foldout, nameof(_foldout.text), BindingMode.ToTarget);
            _foldout.style.color = player.Color.Value;
            player.Power.BindView(_power, nameof(_power.value), BindingMode.ToTarget);
            player.Agility.BindView(_agility, nameof(_agility.value), BindingMode.ToTarget);
            player.Intelligence.BindView(_intelligence, nameof(_intelligence.value), BindingMode.ToTarget);
        }

        private void OnPlayerDeselected(PlayerModel player)
        {
            _foldout.Unbind();
            _foldout.style.color = Color.gray;
            _foldout.text = string.Empty;
            _power.Unbind();
            _agility.Unbind();
            _intelligence.Unbind();
        }
    }
}
