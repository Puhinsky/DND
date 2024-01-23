using Puhinsky.DND.Models;
using UnityEngine;

namespace Puhinsky.DND.UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MapView : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private MapModel _model;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();   
        }

        public void SetModel(MapModel model)
        {
            _model = model;
            _model.Texture.Changed += (Texture2D texture) => OnUpdate();
            _model.PixelsPerUnit.Changed += (int pixelsPerUnit) => OnUpdate();
        }

        private void OnUpdate()
        {
            if (_model.Texture.Value == null)
                return;

            _renderer.sprite = Sprite.Create(_model.Texture.Value, new Rect(0, 0, _model.Texture.Value.width, _model.Texture.Value.height), new Vector2(0.5f, 0.5f), _model.PixelsPerUnit.Value, 0, SpriteMeshType.FullRect);
        }
    }
}
