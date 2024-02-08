using Puhinsky.DND.Models;
using TMPro;
using UnityEngine;

namespace Puhinsky.DND.UI
{
    [RequireComponent(typeof(MouseDraggable))]
    [RequireComponent(typeof(Selectable))]
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        private PlayerSelectionHandler _selectionHandler;
        private SpriteRenderer _renderer;
        private Canvas _canvas;
        private ICharacter _character;
        private Selectable _selectable;

        protected virtual void Awake()
        {
            _selectionHandler = FindAnyObjectByType<PlayerSelectionHandler>();
            _renderer = GetComponent<SpriteRenderer>();
            _canvas = GetComponentInChildren<Canvas>();
            _selectable = GetComponent<Selectable>();

            _selectable.Highlighted.AddListener(OnHighlighted);
            _selectable.Unhighlighted.AddListener(OnUnhighlighed);
            _selectable.Selected.AddListener(OnSelected);
            _selectable.Deselected.AddListener(OnDeselected);
        }

        public void BindModel(ICharacter model)
        {
            _character = model;

            _character.Color.Changed += OnColorChanged;
            OnColorChanged(_character.Color.Value);

            _character.Name.Changed += OnNameChanged;
            OnNameChanged(_character.Name.Value);
        }

        public static CharacterView Instance(string assetName = "Character")
        {
            var view = Instantiate(Resources.Load<CharacterView>(assetName));
            view.GetComponent<MouseDraggable>().SetPosition(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f));

            return view;
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

        private void OnHighlighted()
        {
            _canvas.gameObject.SetActive(true);
        }

        private void OnUnhighlighed()
        {
            _canvas.gameObject.SetActive(false);
        }

        private void OnSelected()
        {
            _selectionHandler.OnPlayerMouseDown(_character);
        }

        private void OnDeselected()
        {
            _selectionHandler.OnPlayerMouseUp(_character);
        }

        private void OnDestroy()
        {
            _selectable.Highlighted.RemoveListener(OnHighlighted);
            _selectable.Unhighlighted.RemoveListener(OnUnhighlighed);
            _selectable.Selected.RemoveListener(OnSelected);
            _selectable.Deselected.RemoveListener(OnDeselected);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
