using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SetupGameMenu : MonoBehaviour
{
    private VisualElement _root;

    private const string _tabCssClass = "setup-tab";

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
    }

    private void Start()
    {
        var tabbedView = new TabView();

        var characteristicsTab = new TabController("��������������", _tabCssClass);
        var characterTab = new TabController("���������", _tabCssClass);
        var mapTab = new TabController("�����", _tabCssClass);

        characteristicsTab.Build(tabbedView, true);
        characterTab.Build(tabbedView);
        mapTab.Build(tabbedView);

        var characteristicsList = new CharacteristicsListController("���������� ��������������");

        characteristicsList.Build(characteristicsTab.GetTarget());

        _root.Add(tabbedView);
    }
}
