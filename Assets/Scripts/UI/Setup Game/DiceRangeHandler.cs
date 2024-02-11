using UnityEngine;

namespace Puhinsky.DND.UI
{
    public class DiceRangeHandler : MonoBehaviour
    {
        [SerializeField] private DiceConfig _config;

        public DiceConfig Config =>_config;
    }
}
