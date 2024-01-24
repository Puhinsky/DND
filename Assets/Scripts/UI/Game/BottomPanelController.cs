using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class BottomPanelController : VisualElement
    {
        public BottomPanelController()
        {
            style.alignSelf = Align.FlexEnd;
            Add(new DiceController());
        }
    }
}
