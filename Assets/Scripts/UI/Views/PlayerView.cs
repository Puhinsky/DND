using Puhinsky.DND.Core.GameState;
using Puhinsky.DND.Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Puhinsky.DND.UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerView : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private const string _spritePath = "Circle";
        private GameModel _gameModel;

        private void Awake()
        {
            _gameModel = FindAnyObjectByType<GameStateMachine>().Model;
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = Resources.Load<Sprite>(_spritePath);
            GetComponent<CircleCollider2D>().radius = _renderer.size.x / 2;
        }

        private void OnMouseDrag()
        {
            if (_gameModel.State.Value == GameStateType.Game)
                SetPosition(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }

        private void SetPosition(Vector3 rawPosition)
        {
            transform.position = new Vector3(rawPosition.x, rawPosition.y, transform.position.z);
        }

        public static PlayerView Instance()
        {
            var gameObject = new GameObject("Player View");
            var view = gameObject.AddComponent<PlayerView>();

            return view;
        }

        public void BindModel(PlayerModel model)
        {
            model.Color.Changed += OnColorChanged;
            OnColorChanged(model.Color.Value);
        }

        private void OnColorChanged(Color color)
        {
            _renderer.color = color;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
