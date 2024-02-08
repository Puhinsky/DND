using UnityEngine;

namespace Puhinsky.DND.Models
{
    public abstract class ICharacterWrapper<T> : ScriptableObject where T : ICharacter, new()
    {
        [SerializeField] private string _name;
        [SerializeField] private Color _color;
        [SerializeField] private int _power;
        [SerializeField] private int _agility;
        [SerializeField] private int _intelligence;
        [SerializeField] private int _stamina;
        [SerializeField] private int _magic;
        [SerializeField] private int _fortune;
        [SerializeField] private int _charisma;

        [SerializeField] private T _preview = new();

        public T Preview => _preview;

        public T Instantiate()
        {
            var character = new T();
            SetCharacteristics(character);

            return character;
        }

        private void OnValidate()
        {
            SetCharacteristics(_preview);
        }

        protected void SetCharacteristics(ICharacter character)
        {
            character.Name.Value = _name;
            character.Color.Value = _color;
            character.Power.Value = _power;
            character.Agility.Value = _agility;
            character.Intelligence.Value = _intelligence;
            character.Stamina.Value = _stamina;
            character.Magic.Value = _magic;
            character.Fortune.Value = _fortune;
            character.Charisma.Value = _charisma;
        }
    }
}
