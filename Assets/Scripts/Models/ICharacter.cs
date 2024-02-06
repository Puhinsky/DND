using Puhinsky.DND.Core;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public interface ICharacter
    {
        public ReactiveProperty<string> Name { get; }
        public ReactiveProperty<Color> Color { get; }
        public ReactiveProperty<int> Power { get; }
        public ReactiveProperty<int> Agility { get; }
        public ReactiveProperty<int> Intelligence { get; }
        public ReactiveProperty<int> Stamina { get; }
        public ReactiveProperty<int> Magic { get; }
        public ReactiveProperty<int> Fortune { get; }
        public ReactiveProperty<int> Charisma { get; }
        public ReactiveProperty<int> Damage { get; }
        public ReactiveProperty<int> Mana { get; }
        public ReactiveProperty<int> Speed { get; }
        public ReactiveProperty<int> Evasion { get; }
        public ReactiveProperty<int> MagicDamage { get; }
        public ReactiveProperty<int> Health { get; }

        public void ApplyDamage(int damage);
        public void ApplyHeal(int heal);
        public void ResetHealth();
    }
}
