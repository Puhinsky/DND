using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Puhinsky.DND.UI
{
    [Serializable]
    public class DiceRangeSetup
    {
        public int MinValue => _minValue;
        public string Status => _status;
        public Color Color => _color;

        [SerializeField] private int _minValue;
        [SerializeField] private string _status;
        [SerializeField] private Color _color;
        [SerializeField] private List<AudioClip> _soundEffects;

        public AudioClip GetSoundEffect()
        {
            return _soundEffects[Random.Range(0, _soundEffects.Count)];
        }
    }
}
