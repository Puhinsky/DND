using static UnityEngine.InputSystem.InputAction;

namespace Puhinsky.DND.UI
{
    public class NPCView : CharacterView
    {
        private Selectable _selectable;
        private GameInputs _gameInputs;

        public static readonly string AssetName = "NPC";

        protected override void Awake()
        {
            base.Awake();

            _selectable = GetComponent<Selectable>();
            _gameInputs = new GameInputs();
            _gameInputs.GameMap.Delete.performed += OnDelete;
        }

        private void OnEnable()
        {
            _gameInputs.Enable();
        }

        private void OnDisable()
        {
            _gameInputs.Disable();
        }

        private void OnDelete(CallbackContext obj)
        {
            if (_selectable.IsHighlighted)
                Destroy(gameObject);
        }
    }
}
