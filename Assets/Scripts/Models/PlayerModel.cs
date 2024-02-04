using Puhinsky.DND.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class PlayerModel
    {
        public ReactiveProperty<string> Name { get; private set; } = new("Player");
        public ReactiveProperty<Color> Color { get; private set; } = new(UnityEngine.Color.black);
        public PreprocessorReactiveProperty<int> Power { get; set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Agility { get; set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Intelligence { get; set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Stamina { get; set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Magic { get; set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Fortune { get; set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Charisma { get; set; } = new(_minPointsByCharacteristic);

        public int Damage => throw new NotImplementedException();
        public int Health => throw new NotImplementedException();
        public int Mana => throw new NotImplementedException();
        public int Speed => throw new NotImplementedException();

        private readonly List<PreprocessorReactiveProperty<int>> _staticCharacteristics = new();

        private const int _minPointsByCharacteristic = 1;
        private const int _maxTotalPoints = 20;

        public PlayerModel()
        {
            _staticCharacteristics.Add(Power);
            _staticCharacteristics.Add(Agility);
            _staticCharacteristics.Add(Intelligence);
            _staticCharacteristics.Add(Stamina);
            _staticCharacteristics.Add(Magic);
            _staticCharacteristics.Add(Fortune);
            _staticCharacteristics.Add(Charisma);

            foreach (var characterstic in _staticCharacteristics)
            {
                characterstic.SetPreprocessor(GetProprocessor(characterstic));
            }
        }

        private PreprocessorReactiveProperty<int>.Preprocessor GetProprocessor(ReactiveProperty<int> characteristic)
        {
            return (value) => Mathf.Clamp(value, _minPointsByCharacteristic, _maxTotalPoints - GetPointWithout(characteristic));
        }

        private int GetPointWithout(ReactiveProperty<int> value)
        {
            return _staticCharacteristics.Where(x => x != value).Select(x => x.Value).Sum();
        }
    }
}
