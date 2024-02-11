using Puhinsky.DND.Core;
using Puhinsky.DND.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    [UxmlElement]
    public partial class DiceController : VisualElement
    {
        public DiceModel Model { get; private set; } = new DiceModel(1, 20);
        private readonly DiceRangeHandler _rangeHandler;
        private readonly AudioPlayer _audio;

        private readonly Button _throwButton = new() { text = _throwButtonLabel };
        private readonly Label _statusLabel = new();
        private readonly Label _diceLabel = new("?");
        private readonly Toggle _volume = new() { value = true };

        private const string _foldoutLabel = "Кость";
        private const string _throwButtonLabel = "Бросить";

        private const string _diceCssClass = "dice";
        private const string _diceLabelCssClass = "dice__label";
        private const string _diceButtonCssClass = "dice__button";
        private const string _diceStatusCssClass = "dice__status";
        private const string _diceVolumeCssClass = "dice__volume-toggle";

        public DiceController()
        {
            _rangeHandler = Object.FindAnyObjectByType<DiceRangeHandler>();
            _audio = Object.FindAnyObjectByType<AudioPlayer>();

            var foldout = new Foldout()
            {
                text = _foldoutLabel,
                value = false
            };

            foldout.Add(_volume);
            foldout.Add(_diceLabel);
            foldout.Add(_statusLabel);
            foldout.Add(_throwButton);
            Add(foldout);

            AddToClassList(_diceCssClass);
            _diceLabel.AddToClassList(_diceLabelCssClass);
            _throwButton.AddToClassList(_diceButtonCssClass);
            _statusLabel.AddToClassList(_diceStatusCssClass);
            _volume.AddToClassList(_diceVolumeCssClass);

            _throwButton.clicked += () => Model.Throw();
            Model.DiceValue.Changed += OnDiceValueChanged;

            SetVolume(_volume.value);
            _volume.RegisterValueChangedCallback(OnVolumeToggle);
        }

        private void OnDiceValueChanged(int value)
        {
            _diceLabel.text = value.ToString();
            var range = _rangeHandler.Config.GetRange(value);
            _audio.Play(range.GetSoundEffect());
            _statusLabel.text = range.Status;
            _statusLabel.style.color = range.Color;
        }

        private void OnVolumeToggle(ChangeEvent<bool> ev)
        {
            SetVolume(ev.newValue);
        }

        private void SetVolume(bool isEnable)
        {
            if (isEnable)
                _audio.Enable();
            else
                _audio.Disable();
        }
    }
}
