using Puhinsky.DND.Core;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class DiceModel
    {
        public ReactiveProperty<int> DiceValue { get; private set; } = new();

        private readonly int _min;
        private readonly int _max;

        public DiceModel(int min, int max)
        {
            _max = max;
            _min = min;
        }

        public void Throw()
        {
            DiceValue.Value = Random.Range(_min, _max + 1);
        }
    }
}
