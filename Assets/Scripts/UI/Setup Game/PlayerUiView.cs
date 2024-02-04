using Puhinsky.DND.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    [UxmlElement]
    public partial class PlayerUiView : VisualElement
    {
        private PlayerModel _model;

        private readonly TextField _name = new() { label = PlayerLabels.Name };
        private readonly ColorPickView _colorPickView = new();
        private readonly IntegerField _power = new() { label = PlayerLabels.Power };
        private readonly IntegerField _agility = new() { label= PlayerLabels.Agility };
        private readonly IntegerField _intelligence = new() { label = PlayerLabels.Intelligence };
        private readonly IntegerField _stamina = new() { label = PlayerLabels.Stamina };
        private readonly IntegerField _magic = new() { label = PlayerLabels.Magic };
        private readonly IntegerField _fortune = new() { label = PlayerLabels.Fortune };
        private readonly IntegerField _charisma = new() { label = PlayerLabels.Charisma };
        private readonly IntegerField _total = new() { label = "Всего", isReadOnly = true };
        private readonly IntegerField _damage = new() { label = PlayerLabels.Damage, isReadOnly = true };
        private readonly IntegerField _health = new() { label = PlayerLabels.Health, isReadOnly = true };
        private readonly IntegerField _mana = new() { label = PlayerLabels.Mana, isReadOnly = true };
        private readonly IntegerField _speed = new() { label = PlayerLabels.Speed, isReadOnly = true };
        private readonly IntegerField _evasion = new() { label = PlayerLabels.Evasion, isReadOnly = true };
        private readonly IntegerField _magicDamage = new() { label = PlayerLabels.MagicDamage, isReadOnly = true };

        private const string _containerCssClass = "player-container";

        public PlayerUiView()
        {
            var container = new VisualElement();
            container.AddToClassList(_containerCssClass);
            Add(container);

            container.Add(_name);
            _colorPickView.Changed += OnPickColorChanged;
            container.Add(_colorPickView);
            var foldout = new Foldout
            {
                text = "Характеристики",
                value = false
            };
            foldout.Add(_power);
            foldout.Add(_agility);
            foldout.Add(_intelligence);
            foldout.Add(_stamina);
            foldout.Add(_magic);
            foldout.Add(_fortune);
            foldout.Add(_charisma);
            foldout.Add(_total);
            foldout.Add(_damage);
            foldout.Add(_health);
            foldout.Add(_mana);
            foldout.Add(_speed);
            foldout.Add(_evasion);
            foldout.Add(_magicDamage);
            container.Add(foldout);
        }

        public void BindModel(PlayerModel model)
        {
            _model = model;
            BindModelInternal();
        }

        public void UnbindModel()
        {
            _name.dataSource = null;
            _model.Color.Changed -= OnColorChanged;
            _model = null;
        }

        private void BindModelInternal()
        {
            _model.Name.BindView(_name, nameof(_name.value), BindingMode.TwoWay);
            _model.Power.BindView(_power, nameof(_power.value), BindingMode.TwoWay);
            _model.Agility.BindView(_agility, nameof(_agility.value), BindingMode.TwoWay);
            _model.Intelligence.BindView(_intelligence, nameof(_intelligence.value), BindingMode.TwoWay);
            _model.Stamina.BindView(_stamina, nameof(_stamina.value), BindingMode.TwoWay);
            _model.Magic.BindView(_magic, nameof(_magic.value), BindingMode.TwoWay);
            _model.Fortune.BindView(_fortune, nameof(_fortune.value), BindingMode.TwoWay);
            _model.Charisma.BindView(_charisma, nameof(_charisma.value), BindingMode.TwoWay);
            _model.StaticTotal.BindView(_total, nameof(_total.value), BindingMode.ToTarget);
            _model.Damage.BindView(_damage, nameof(_damage.value), BindingMode.ToTarget);
            _model.DefaultHealth.BindView(_health, nameof(_health.value), BindingMode.ToTarget);
            _model.Mana.BindView(_mana, nameof(_mana.value), BindingMode.ToTarget);
            _model.Speed.BindView(_speed, nameof(_speed.value), BindingMode.ToTarget);
            _model.Evasion.BindView(_evasion, nameof(_evasion.value), BindingMode.ToTarget);
            _model.MagicDamage.BindView(_magicDamage, nameof(_magicDamage.value), BindingMode.ToTarget);

            _model.Color.Changed += OnColorChanged;
            _colorPickView.SetColor(_model.Color.Value);
            OnColorChanged(_model.Color.Value);
        }

        private void OnColorChanged(Color color)
        {
            style.backgroundColor = color;
        }

        private void OnPickColorChanged(Color color)
        {
            _model.Color.Value = color;
        }
    }
}
