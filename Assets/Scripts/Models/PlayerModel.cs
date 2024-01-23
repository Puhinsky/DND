using Puhinsky.DND.Core;
using System;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class PlayerModel
    {
        public ReactiveProperty<string> Name { get; private set; } = new("Player");
        public ReactiveProperty<Color> Color { get; private set; } = new(UnityEngine.Color.black);
        public int Power { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Stamina { get; set; }
        public int Magic { get; set; }
        public int Fortune { get; set; }
        public int Charisma { get; set; }

        public int Damage => throw new NotImplementedException();
        public int Health => throw new NotImplementedException();
        public int Mana => throw new NotImplementedException();
        public int Speed => throw new NotImplementedException();
    }
}
