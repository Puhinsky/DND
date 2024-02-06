using Puhinsky.DND.Core;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class MapModel
    {
        public ReactiveProperty<Texture2D> Texture { get; private set; } = new();
        public PreprocessorReactiveProperty<int> PixelsPerUnit { get; private set; }

        public int MinPixelPerUnit { get; private set; } = 1;
        public int MaxPixelPerUnit { get; private set; } = 1000;

        public MapModel()
        {
            PixelsPerUnit = new((x) => Mathf.Clamp(x, MinPixelPerUnit, MaxPixelPerUnit), 100);
        }
    }
}
