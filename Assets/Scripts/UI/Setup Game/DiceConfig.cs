using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puhinsky.DND.UI
{
    [CreateAssetMenu(menuName = "DND/Dice/Create Config", fileName = "Dice Config")]
    public class DiceConfig : ScriptableObject
    {
        [SerializeField] private List<DiceRangeSetup> _ranges;

        public DiceRangeSetup GetRange(int value)
        {
            foreach (var range in _ranges)
            {
                if (range.MinValue <= value)
                    return range;
            }

            return _ranges.Last();
        }

        private void OnValidate()
        {
            _ranges = _ranges.OrderByDescending(x => x.MinValue).ToList();
        }
    }
}
