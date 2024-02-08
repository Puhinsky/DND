using UnityEngine;

namespace Puhinsky.DND.UI
{
    [RequireComponent(typeof(Renderer))]
    public class ColorPropertyBlock : MonoBehaviour
    {
        [SerializeField] private string _propertyName;

        private Renderer _renderer;
        private MaterialPropertyBlock _block;

        private void Awake()
        {
            _block = new();
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            _renderer.SetPropertyBlock(_block);
        }

        public void SetColor(Color color)
        {
            _block.SetColor(_propertyName, color);
        }
    }
}
