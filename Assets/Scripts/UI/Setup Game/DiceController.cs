using Puhinsky.DND.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    [UxmlElement]
    public partial class DiceController : VisualElement
    {
        public DiceModel Model { get; private set; } = new DiceModel(1, 20);

        private readonly Button _throwButton = new() { text = _throwButtonLabel };
        private readonly Label _statusLabel = new();
        private readonly Label _diceLabel = new("?");

        private const string _foldoutLabel = "Кость";
        private const string _throwButtonLabel = "Бросить";

        private const string _diceCssClass = "dice";
        private const string _diceLabelCssClass = "dice__label";
        private const string _diceButtonCssClass = "dice__button";
        private const string _diceStatusCssClass = "dice__status";

        public DiceController()
        {
            var foldout = new Foldout()
            {
                text = _foldoutLabel,
                value = false
            };

            foldout.Add(_diceLabel);
            foldout.Add(_statusLabel);
            foldout.Add(_throwButton);
            Add(foldout);

            AddToClassList(_diceCssClass);
            _diceLabel.AddToClassList(_diceLabelCssClass);
            _throwButton.AddToClassList(_diceButtonCssClass);
            _statusLabel.AddToClassList(_diceStatusCssClass);

            _throwButton.clicked += () => Model.Throw();
            Model.DiceValue.Changed += OnDiceValueChanged;
        }

        private void OnDiceValueChanged(int value)
        {
            _diceLabel.text = value.ToString();
            var status = GetStatus(value);
            _statusLabel.text = status.Text;
            _statusLabel.style.color = status.Color;
        }

        private Status GetStatus(int value)
        {
            if (value.InRange(1, 2))
                return new Status { Text = "Критическая неудача", Color = Color.red };

            if (value.InRange(3, 8))
                return new Status { Text = "Неудача", Color = Color.yellow };

            if(value.InRange(9,12))
                return new Status { Text = "Норм", Color = Color.blue };

            if (value.InRange(13, 18))
                return new Status { Text = "Удача", Color = Color.green };

            if (value.InRange(19, 20))
                return new Status { Text = "Критическая удача", Color = Color.magenta };

            return new Status { Text = "Ты как сюда попал? ", Color = Color.black };
        }

        private struct Status
        {
            public string Text;
            public Color Color;
        }
    }

    public static class IntExtension
    {
        public static bool InRange(this int value, int min, int max)
        {
            if (value >= min && value <= max)
                return true;
            else
                return false;
        }
    }
}
