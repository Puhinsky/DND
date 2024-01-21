using UnityEngine.UIElements;

public class TabController
{
    private readonly Tab _tab;

    public TabController(string tabLabel, string cssClass)
    {
        _tab = new(tabLabel);
        _tab.tabHeader.AddToClassList(cssClass);
    }

    public void Build(TabView tabView, bool isActive = false)
    {
        tabView.Add(_tab);
    }

    public VisualElement GetTarget() => _tab;
}
