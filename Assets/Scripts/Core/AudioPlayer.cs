using UnityEngine;

namespace Puhinsky.DND.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource _audioPlayer;

        private void Awake()
        {
            _audioPlayer = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            _audioPlayer.Stop();
            _audioPlayer.PlayOneShot(clip);
        }

        public void Enable()
        {
            if (_audioPlayer == null)
                return;

            _audioPlayer.volume = 1.0f;
        }

        public void Disable()
        {
            if (_audioPlayer == null)
                return;

            _audioPlayer.volume = 0f;
        }
    }
}
