using Puhinsky.DND.Core;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class MapModel
    {
        public ReactiveProperty<Texture2D> Texture { get; private set; } = new();
        public ReactiveProperty<int> PixelsPerUnit { get; private set; } = new(100);

        public int MinPixelPerUnit { get; private set; } = 1;
        public int MaxPixelPerUnit { get; private set; } = 1000;

        public MapModel()
        {
            PixelsPerUnit.Changed += (int value) =>
            {
                if (value < MinPixelPerUnit)
                    PixelsPerUnit.Value = MinPixelPerUnit;
                else if (value > MaxPixelPerUnit)
                    PixelsPerUnit.Value = MaxPixelPerUnit;
            };
        }
    }
}
