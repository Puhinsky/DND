using UnityEngine;

namespace Puhinsky.DND.Core
{
    public class OrtographicBillboard : MonoBehaviour
    {
        [SerializeField] private float _ratio = 1f;
        [SerializeField] private float _minSize = 0.1f;
        [SerializeField] private float _maxSize = 2f;

        private void Update()
        {
            var size = Mathf.Clamp(_ratio * Camera.main.orthographicSize, _minSize, _maxSize);
            transform.localScale = size * Vector3.one;
        }
    }
}
