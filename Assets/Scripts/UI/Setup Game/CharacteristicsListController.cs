using UnityEngine;
using UnityEngine.UIElements;

public class CharacteristicsListController : ListController<CharacteristicModel, TextField>
{
    private readonly Color[] _colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
    };

    private const string itemCssClass = "characteristic";
    private const string _textInputClass = "unity-text-input";

    public CharacteristicsListController(string listLabel) : base(listLabel, itemCssClass) { }

    protected override void OnBindItem(TextField view, int index)
    {
        Models[index].Name = view.text;
        view.Q<VisualElement>(_textInputClass).style.backgroundColor = _colors[index % _colors.Length];
    }
}
