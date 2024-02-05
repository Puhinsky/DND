using Puhinsky.DND.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class PlayerModel
    {
        public ReactiveProperty<string> Name { get; private set; } = new("Player");
        public ReactiveProperty<Color> Color { get; private set; } = new(UnityEngine.Color.black);
        public PreprocessorReactiveProperty<int> Power { get; private set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Agility { get; private set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Intelligence { get; private set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Stamina { get; private set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Magic { get; private set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Fortune { get; private set; } = new(_minPointsByCharacteristic);
        public PreprocessorReactiveProperty<int> Charisma { get; private set; } = new(_minPointsByCharacteristic);
        public DependentReactiveProperty<int> StaticTotal { get; private set; }

        public DependentReactiveProperty<int> Damage { get; private set; }
        public DependentReactiveProperty<int> DefaultHealth { get; private set; }
        public DependentReactiveProperty<int> Mana { get; private set; }
        public DependentReactiveProperty<int> Speed { get; private set; }
        public DependentReactiveProperty<int> Evasion { get; private set; }
        public DependentReactiveProperty<int> MagicDamage { get; private set; }

        public PreprocessorReactiveProperty<int> Health { get; private set; } = new((x) => Mathf.Max(x, 0));

        private readonly List<PreprocessorReactiveProperty<int>> _staticCharacteristics = new();

        private const int _minPointsByCharacteristic = 1;
        private const int _maxTotalPoints = 27;

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

            StaticTotal = new(() => _staticCharacteristics.Sum(x => x.Value));
            StaticTotal.AddDependency(Power, Agility, Intelligence, Stamina, Magic, Fortune, Charisma);

            Damage = new(() => 1 + 2 * Power.Value);
            Damage.AddDependency(Power);

            DefaultHealth = new(() => 10 + 10 * Power.Value + 10 * Stamina.Value);
            DefaultHealth.AddDependency(Power, Stamina);
            DefaultHealth.TypelessChanged += ResetHealth;

            Mana = new(() => 10 + 5 * Magic.Value);
            Mana.AddDependency(Magic);

            Speed = new(() => 4 + 2 * Agility.Value + 3 * Stamina.Value);
            Speed.AddDependency(Agility, Stamina);

            Evasion = new(() => Agility.Value + Fortune.Value);
            Evasion.AddDependency(Agility, Fortune);

            MagicDamage = new(() => Mana.Value / 2 + Intelligence.Value);
            MagicDamage.AddDependency(Mana, Intelligence);

            ResetHealth();
        }

        private PreprocessorReactiveProperty<int>.Preprocessor GetProprocessor(ReactiveProperty<int> characteristic)
        {
            return (value) => Mathf.Clamp(value, _minPointsByCharacteristic, _maxTotalPoints - GetPointsWithout(characteristic));
        }

        private int GetPointsWithout(ReactiveProperty<int> value)
        {
            return _staticCharacteristics.Where(x => x != value).Sum(x => x.Value);
        }

        public void ApplyDamage(int damage)
        {
            Health.Value -= damage;
        }

        public void ApplyHeal(int heal)
        {
            Health.Value += heal;
        }

        public void ResetHealth()
        {
            Health.Value = DefaultHealth.Value;
        }
    }
}
