using Puhinsky.DND.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class NPCDatabaseController : VisualElement
    {
        private const string _npcSelectionCssClass = "npc-select";

        public NPCDatabaseController()
        {
            var foldout = new Foldout() {
                text = "Не люди",
                value = false
            };

            var scrollView = new ScrollView();
            foldout.Add(scrollView);

            Add(foldout);
            AddToClassList(_npcSelectionCssClass);

            var database = Object.FindAnyObjectByType<NPCDatabase>();

            foreach (var template in database.Templates)
            {
                scrollView.Add(new NPCTemplateView(template));
            }
        }
    }
}
