using Puhinsky.DND.Core;
using Puhinsky.DND.Models;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    [UxmlElement]
    public partial class PlayerUiView : VisualElement
    {
        private PlayerModel _model;

        private readonly TextField _name = new();
        private readonly ColorPickView _colorPickView = new();
        private readonly IntegerField _power = new();

        private const string _nameLabel = "Имя";
        private const string _powerLabel = "Сила";

        public PlayerUiView()
        {
            _name.label = _nameLabel;
            Add(_name);
            _colorPickView.Changed += OnPickColorChanged;
            Add(_colorPickView);
            var foldout = new Foldout
            {
                value = false
            };
            _power.label = _powerLabel;
            foldout.Add(_power);
            Add(foldout);
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
