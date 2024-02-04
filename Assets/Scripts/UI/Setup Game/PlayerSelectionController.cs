using Puhinsky.DND.Models;
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
        private readonly IntegerField _stamina = new() { label = PlayerLabels.Stamina, isReadOnly = true };
        private readonly IntegerField _magic = new() { label = PlayerLabels.Magic, isReadOnly = true };
        private readonly IntegerField _fortune = new() { label = PlayerLabels.Fortune, isReadOnly = true };
        private readonly IntegerField _charisma = new() { label = PlayerLabels.Charisma, isReadOnly = true };
        private readonly IntegerField _damage = new() { label = PlayerLabels.Damage, isReadOnly = true };
        private readonly IntegerField _mana = new() { label = PlayerLabels.Mana, isReadOnly = true };
        private readonly IntegerField _health = new() { label = PlayerLabels.Health, isReadOnly = true };
        private readonly IntegerField _speed = new() { label = PlayerLabels.Speed, isReadOnly = true };
        private readonly IntegerField _evasion = new() { label = PlayerLabels.Evasion, isReadOnly = true };
        private readonly IntegerField _magicDamage = new() { label = PlayerLabels.MagicDamage, isReadOnly = true };

        private const string _playerSelectCssClass = "player-select";

        public PlayerSelectionController()
        {
            var handler = Object.FindAnyObjectByType<PlayerSelectionHandler>();
            handler.PlayerSelected += OnPlayerSelected;
            handler.PlayerDeselected += OnPlayerDeselected;

            _foldout.Add(_power);
            _foldout.Add(_agility);
            _foldout.Add(_intelligence);
            _foldout.Add(_stamina);
            _foldout.Add(_magic);
            _foldout.Add(_fortune);
            _foldout.Add(_charisma);
            _foldout.Add(_damage);
            _foldout.Add(_mana);
            _foldout.Add(_health);
            _foldout.Add(_speed);
            _foldout.Add(_evasion);
            _foldout.Add(_magicDamage);
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
            player.Stamina.BindView(_stamina, nameof(_stamina.value), BindingMode.ToTarget);
            player.Magic.BindView(_magic, nameof(_magic.value), BindingMode.ToTarget);
            player.Fortune.BindView(_fortune, nameof(_fortune.value), BindingMode.ToTarget);
            player.Charisma.BindView(_charisma, nameof(_charisma.value), BindingMode.ToTarget);
            player.Damage.BindView(_damage, nameof(_damage.value), BindingMode.ToTarget);
            player.Mana.BindView(_mana, nameof(_mana.value), BindingMode.ToTarget);
            player.Health.BindView(_health, nameof(_health.value), BindingMode.ToTarget);
            player.Speed.BindView(_speed, nameof(_speed.value), BindingMode.ToTarget);
            player.Evasion.BindView(_evasion, nameof(_evasion.value), BindingMode.ToTarget);
            player.MagicDamage.BindView(_magicDamage, nameof(_magicDamage.value), BindingMode.ToTarget);
        }

        private void OnPlayerDeselected(PlayerModel player)
        {
            _foldout.style.color = Color.gray;
        }
    }
}
