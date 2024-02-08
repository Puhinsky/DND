using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class TopPanelController : VisualElement
    {
        public TopPanelController()
        {
            style.flexDirection = FlexDirection.Row;
            style.alignItems = Align.FlexStart;
            style.justifyContent = Justify.SpaceBetween;
            style.flexGrow = 1;

            Add(new NPCDatabaseController());
        }
    }
}
