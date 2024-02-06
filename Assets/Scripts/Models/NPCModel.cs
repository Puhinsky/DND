using Puhinsky.DND.Core;
using System;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    [Serializable]
    public class NPCModel : Character
    {
        public override ReactiveProperty<int> Power => _power;
        public override ReactiveProperty<int> Agility => _agility;
        public override ReactiveProperty<int> Intelligence => _intelligence;
        public override ReactiveProperty<int> Stamina => _stamina;
        public override ReactiveProperty<int> Magic => _magic;
        public override ReactiveProperty<int> Fortune => _fortune;
        public override ReactiveProperty<int> Charisma => _charisma;

        [SerializeField] private MinBoundedIntegerProperty _power = new();
        [SerializeField] private MinBoundedIntegerProperty _agility = new();
        [SerializeField] private MinBoundedIntegerProperty _intelligence = new();
        [SerializeField] private MinBoundedIntegerProperty _stamina = new();
        [SerializeField] private MinBoundedIntegerProperty _magic = new();
        [SerializeField] private MinBoundedIntegerProperty _fortune = new();
        [SerializeField] private MinBoundedIntegerProperty _charisma = new();

        public NPCModel()
        {
            Name = new("NPC");
            Color = new(UnityEngine.Color.gray);
        }
    }
}
