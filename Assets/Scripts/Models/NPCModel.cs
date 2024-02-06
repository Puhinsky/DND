using Puhinsky.DND.Core;

namespace Puhinsky.DND.Models
{
    public class NPCModel : Character
    {
        public override ReactiveProperty<int> Power => _power;
        public override ReactiveProperty<int> Agility => _agility;
        public override ReactiveProperty<int> Intelligence => _intelligence;
        public override ReactiveProperty<int> Stamina => _stamina;
        public override ReactiveProperty<int> Magic => _magic;
        public override ReactiveProperty<int> Fortune => _fortune;
        public override ReactiveProperty<int> Charisma => _charisma;

        private readonly MinBoundedIntegerProperty _power = new();
        private readonly MinBoundedIntegerProperty _agility = new();
        private readonly MinBoundedIntegerProperty _intelligence = new();
        private readonly MinBoundedIntegerProperty _stamina = new();
        private readonly MinBoundedIntegerProperty _magic = new();
        private readonly MinBoundedIntegerProperty _fortune = new();
        private readonly MinBoundedIntegerProperty _charisma = new();

        public NPCModel()
        {
            Name = new("NPC");
            Color = new(UnityEngine.Color.gray);
        }
    }
}
