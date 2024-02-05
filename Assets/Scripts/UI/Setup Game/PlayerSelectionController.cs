using Puhinsky.DND.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class PlayerSelectionController : VisualElement
    {
        private PlayerModel _player;

        private readonly Foldout _foldout = new() { value = false };
        private readonly UnsignedIntegerField _appliedDamage = new() { label = "Нанести" };
        private readonly Button _applyDamage = new() { text = "ОК"};
        private readonly UnsignedIntegerField _appliedHeal = new() { label = "Вылечить" };
        private readonly Button _applyHeal = new() { text = "ОК" };
        private readonly Button _resetHealth = new() { text = "Восстановить" };
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

            var damageContainer = new VisualElement();
            damageContainer.style.flexDirection = FlexDirection.Row;
            damageContainer.Add(_appliedDamage);
            damageContainer.Add(_applyDamage);
            _applyDamage.clicked += () =>
            {
                _player.ApplyDamage((int)_appliedDamage.value);
                _appliedDamage.value = 0;
            };

            var healContainer = new VisualElement();
            healContainer.style.flexDirection = FlexDirection.Row;
            healContainer.Add(_appliedHeal);
            healContainer.Add(_applyHeal);
            _applyHeal.clicked += () =>
            {
                _player.ApplyHeal((int)_appliedHeal.value);
                _appliedHeal.value = 0;
            };

            _resetHealth.clicked += () => _player.ResetHealth();

            var scrollView = new ScrollView();

            scrollView.Add(damageContainer);
            scrollView.Add(healContainer);
            scrollView.Add(_resetHealth);
            scrollView.Add(_power);
            scrollView.Add(_agility);
            scrollView.Add(_intelligence);
            scrollView.Add(_stamina);
            scrollView.Add(_magic);
            scrollView.Add(_fortune);
            scrollView.Add(_charisma);
            scrollView.Add(_damage);
            scrollView.Add(_mana);
            scrollView.Add(_health);
            scrollView.Add(_speed);
            scrollView.Add(_evasion);
            scrollView.Add(_magicDamage);
            _foldout.Add(scrollView);
            Add(_foldout);
            AddToClassList(_playerSelectCssClass);
        }

        private void OnPlayerSelected(PlayerModel player)
        {
            _player = player;

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
