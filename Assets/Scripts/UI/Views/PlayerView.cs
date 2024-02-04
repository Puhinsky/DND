using Puhinsky.DND.Core.GameState;
using Puhinsky.DND.Models;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Puhinsky.DND.UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        private PlayerSelectionHandler _selectionHandler;
        private SpriteRenderer _renderer;
        private GameModel _gameModel;
        private Canvas _canvas;
        private PlayerModel _playerModel;

        private bool _isPressed;

        private void Awake()
        {
            _selectionHandler = FindAnyObjectByType<PlayerSelectionHandler>();
            _gameModel = FindAnyObjectByType<GameStateMachine>().Model;
            _renderer = GetComponent<SpriteRenderer>();
            _canvas = GetComponentInChildren<Canvas>();

            _canvas.gameObject.SetActive(false);
        }

        private void OnMouseDrag()
        {
            if (_gameModel.State.Value == GameStateType.Game)
                SetPosition(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }

        private void OnMouseEnter()
        {
            _canvas.gameObject.SetActive(true);
        }

        private void OnMouseExit()
        {
            if(!_isPressed)
                _canvas.gameObject.SetActive(false);
        }


        private void OnMouseDown()
        {
            _selectionHandler.OnPlayerMouseDown(_playerModel);
            _isPressed = true;
        }

        private void OnMouseUp()
        {
            _selectionHandler.OnPlayerMouseUp(_playerModel);
            _isPressed = false;
        }

        private void SetPosition(Vector3 rawPosition)
        {
            transform.position = new Vector3(rawPosition.x, rawPosition.y, transform.position.z);
        }

        public static PlayerView Instance()
        {
            return Instantiate(Resources.Load<PlayerView>("Player")).GetComponent<PlayerView>();
        }

        public void BindModel(PlayerModel model)
        {
            _playerModel = model;

            model.Color.Changed += OnColorChanged;
            OnColorChanged(model.Color.Value);

            model.Name.Changed += OnNameChanged;
            OnNameChanged(model.Name.Value);
        }

        private void OnColorChanged(Color color)
        {
            _renderer.color = color;
            _label.color = color;
        }

        private void OnNameChanged(string name)
        {
            _label.text = name;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
