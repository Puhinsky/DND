using UnityEngine;
using UnityEngine.Events;

namespace Puhinsky.DND.UI
{
    public class Selectable : MonoBehaviour
    {
        public bool IsHighlighted { get; private set; }
        public bool IsSelected { get; private set; }

        public UnityEvent Highlighted { get; private set; } = new();
        public UnityEvent Unhighlighted { get; private set; } = new();
        public UnityEvent Selected { get; private set; } = new();
        public UnityEvent Deselected { get; private set; } = new();

        private bool _isPressed;

        private void Start()
        {
            Unhighlighted?.Invoke();
        }

        private void OnMouseEnter()
        {
            IsHighlighted = true;
            Highlighted?.Invoke();
        }

        private void OnMouseExit()
        {
            if (!_isPressed)
            {
                IsHighlighted = false;
                Unhighlighted?.Invoke();
            }
        }


        private void OnMouseDown()
        {
            IsSelected = true;
            Selected?.Invoke();
            _isPressed = true;
        }

        private void OnMouseUp()
        {
            IsSelected = false;
            Deselected?.Invoke();
            _isPressed = false;
        }
    }
}
