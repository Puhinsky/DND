using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Puhinsky.DND.Core
{
    [CustomPropertyDrawer(typeof(ReactiveProperty<>), true)]
    public class ReactivePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var field = new PropertyField(property.FindPropertyRelative("_value"), property.displayName);

            return field;
        }
    }
}
