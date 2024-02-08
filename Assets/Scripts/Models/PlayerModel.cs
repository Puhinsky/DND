using Puhinsky.DND.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class PlayerModel : Character
    {
        public override ReactiveProperty<int> Power => _power;
        public override ReactiveProperty<int> Agility => _agility;
        public override ReactiveProperty<int> Intelligence => _intelligence;
        public override ReactiveProperty<int> Stamina => _stamina;
        public override ReactiveProperty<int> Magic => _magic;
        public override ReactiveProperty<int> Fortune => _fortune;
        public override ReactiveProperty<int> Charisma => _charisma;
        public DependentReactiveProperty<int> StaticTotal { get; private set; }

        public PreprocessorReactiveProperty<int> _power = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> _agility = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> _intelligence = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> _stamina = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> _magic = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> _fortune  = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> _charisma = new(_minPointsByCharacteristic);

        private readonly List<PreprocessorReactiveProperty<int>> _staticCharacteristics = new();
        private const int _minPointsByCharacteristic = 1;
        private const int _maxTotalPoints = 27;

        public PlayerModel()
        {
            Name = new("Player");
            Color = new(UnityEngine.Color.black);

            _staticCharacteristics.Add(_power);
            _staticCharacteristics.Add(_agility);
            _staticCharacteristics.Add(_intelligence);
            _staticCharacteristics.Add(_stamina);
            _staticCharacteristics.Add(_magic);
            _staticCharacteristics.Add(_fortune);
            _staticCharacteristics.Add(_charisma);

            foreach (var characterstic in _staticCharacteristics)
            {
                characterstic.SetPreprocessor(GetProprocessor(characterstic));
            }

            StaticTotal = new(() => _staticCharacteristics.Sum(x => x.Value));
            StaticTotal.AddDependency(Power, Agility, Intelligence, Stamina, Magic, Fortune, Charisma);
        }

        private PreprocessorReactiveProperty<int>.Preprocessor GetProprocessor(ReactiveProperty<int> characteristic)
        {
            return (value) => Mathf.Clamp(value, _minPointsByCharacteristic, _maxTotalPoints - GetPointsWithout(characteristic));
        }

        private int GetPointsWithout(ReactiveProperty<int> value)
        {
            return _staticCharacteristics.Where(x => x != value).Sum(x => x.Value);
        }
    }
}
