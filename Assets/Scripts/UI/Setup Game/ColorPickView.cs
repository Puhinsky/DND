using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class ColorPickView : VisualElement
    {
        public event Action<Color> Changed;

        private readonly Label _label = new("Цвет");
        private readonly SliderInt _red = new(0, 255);
        private readonly SliderInt _green = new(0, 255);
        private readonly SliderInt _blue = new(0, 255);

        public ColorPickView()
        {
            style.flexDirection = FlexDirection.Row;
            style.justifyContent = Justify.SpaceBetween;

            _red.label = "Красный";
            _green.label = "Зеленый";
            _blue.label = "Синий";

            _red.RegisterValueChangedCallback(OnValueChanged);
            _green.RegisterValueChangedCallback(OnValueChanged);
            _blue.RegisterValueChangedCallback(OnValueChanged);

            Add(_label);
            Add(_red);
            Add(_green);
            Add(_blue);
        }

        public void SetColor(Color color)
        {
            _red.SetValueWithoutNotify(GetValueFromSource(color.r));
            _green.SetValueWithoutNotify(GetValueFromSource(color.g));
            _blue.SetValueWithoutNotify(GetValueFromSource(color.b));
        }

        private void OnValueChanged(ChangeEvent<int> e)
        {
            Changed?.Invoke(new Color(GetValueFromInput(_red), GetValueFromInput(_green), GetValueFromInput(_blue)));
        }

        private float GetValueFromInput(SliderInt field)
        {
            return Mathf.Clamp(field.value / 255f, 0f, 1f);
        }

        private int GetValueFromSource(float color)
        {
            return (int)(color * 255);
        }
    }
}
