using Puhinsky.DND.Core;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class MinBoundedIntegerProperty : PreprocessorReactiveProperty<int>
    {
        private readonly int _minValue;

        public MinBoundedIntegerProperty(int minValue = 0)
        {
            _minValue = minValue;
            SetPreprocessor(ClampMin);
        }

        public MinBoundedIntegerProperty(int minValue, int value) : base(value)
        {
            _minValue = minValue;
            SetPreprocessor(ClampMin);
        }

        private int ClampMin(int value)
        {
            return Mathf.Max(value, _minValue);
        }
    }
}
