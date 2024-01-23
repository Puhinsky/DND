using Puhinsky.DND.Core;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class MapModel
    {
        public ReactiveProperty<Texture2D> Texture { get; private set; } = new();
        public ReactiveProperty<int> PixelsPerUnit { get; private set; } = new(100);
    }
}
