using Puhinsky.DND.Core;
using System;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    [Serializable]
    public class Character : ICharacter
    {
        [field: SerializeField] public ReactiveProperty<string> Name { get; protected set; }
        [field: SerializeField] public ReactiveProperty<Color> Color { get; protected set; }

        public virtual ReactiveProperty<int> Power { get; }

        public virtual ReactiveProperty<int> Agility { get; }

        public virtual ReactiveProperty<int> Intelligence { get; }

        public virtual ReactiveProperty<int> Stamina { get; }

        public virtual ReactiveProperty<int> Magic { get; }

        public virtual ReactiveProperty<int> Fortune { get; }

        public virtual ReactiveProperty<int> Charisma { get; }

        public ReactiveProperty<int> Damage => _damage;

        public ReactiveProperty<int> Mana => _mana;

        public ReactiveProperty<int> Speed => _speed;

        public ReactiveProperty<int> Evasion => _evasion;

        public ReactiveProperty<int> MagicDamage => _magicDamage;

        public ReactiveProperty<int> Health { get; }

        [SerializeField] private DependentReactiveProperty<int> _damage;
        [SerializeField] private DependentReactiveProperty<int> _mana;
        [SerializeField] private DependentReactiveProperty<int> _speed;
        [SerializeField] private DependentReactiveProperty<int> _evasion;
        [SerializeField] private DependentReactiveProperty<int> _magicDamage;
        [SerializeField] private DependentReactiveProperty<int> _defaultHealth;
        private readonly MinBoundedIntegerProperty _health = new();

        public Character()
        {
            _damage = new(() => 1 + 2 * Power.Value);
            _damage.AddDependency(Power);

            _defaultHealth = new(() => 10 + 10 * Power.Value + 10 * Stamina.Value);
            _defaultHealth.AddDependency(Power, Stamina);
            _defaultHealth.TypelessChanged += ResetHealth;

            _mana = new(() => 10 + 5 * Magic.Value);
            _mana.AddDependency(Magic);

            _speed = new(() => 4 + 2 * Agility.Value + 3 * Stamina.Value);
            _speed.AddDependency(Agility, Stamina);

            _evasion = new(() => Agility.Value + Fortune.Value);
            _evasion.AddDependency(Agility, Fortune);

            _magicDamage = new(() => Mana.Value / 2 + Intelligence.Value);
            _magicDamage.AddDependency(Mana, Intelligence);

            ResetHealth();
        }

        public void ApplyDamage(int damage)
        {
            _health.Value -= damage;
        }

        public void ApplyHeal(int heal)
        {
           _health.Value += heal;
        }

        public void ResetHealth()
        {
            _health.Value = _defaultHealth.Value;
        }
    }
}
