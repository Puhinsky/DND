using UnityEngine.UIElements;

namespace Puhinsky.DND.Core
{
    public interface IVisualBindable
    {
        public void BindView(VisualElement view, BindingId binding, BindingMode mode);
    }
}
