using Puhinsky.DND.Models;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class NPCTemplateView : Button
    {
        private readonly NPCWrapper _wrapper;

        private const string _iconCssClass = "npc-template__icon";
        private const string _npcTemplateCssClass = "npc-template";

        public NPCTemplateView(NPCWrapper npcWrapper)
        {
            _wrapper = npcWrapper;

            var icon = new VisualElement();
            icon.style.backgroundColor = npcWrapper.Preview.Color.Value;
            Add(icon);
            icon.AddToClassList(_iconCssClass);

            var label = new Label();
            npcWrapper.Preview.Name.BindView(label, nameof(label.text), BindingMode.ToTarget);
            Add(label);

            AddToClassList(_npcTemplateCssClass);
            RegisterCallback<ClickEvent>(OnCreate);
        }

        private void OnCreate(ClickEvent ev)
        {
            var character = CharacterView.Instance(NPCView.AssetName);
            character.BindModel(_wrapper.Instantiate());
        }
    }
}
