using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class BottomPanelController : VisualElement
    {
        public BottomPanelController()
        {
            style.alignSelf = Align.FlexEnd;
            style.flexDirection = FlexDirection.Row;
            style.alignItems = Align.FlexEnd;
            style.justifyContent = Justify.SpaceBetween;
            style.flexGrow = 1;
            Add(new DiceController());
            Add(new PlayerSelectionController());
        }
    }
}
