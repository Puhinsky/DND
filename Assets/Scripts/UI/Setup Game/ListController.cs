using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public abstract class ListController<TModel, TView> where TModel : class, new() where TView : VisualElement, new()
{
    private readonly VisualElement _container = new();
    private readonly ListView _listView = new();
    private readonly List<TModel> _models = new();

    public IReadOnlyList<TModel> Models => _models;

    private const string _addItemLabel = "+";
    private const string _removeItemLabel = "-";

    public ListController(string listLabel, string itemCssClass)
    {
        var controlButtonsContainer = new VisualElement();
        controlButtonsContainer.style.flexDirection = FlexDirection.Row;
        controlButtonsContainer.Add(GetButtonWithLabel(_addItemLabel, OnAddItem));
        controlButtonsContainer.Add(GetButtonWithLabel(_removeItemLabel, OnRemoveItem));

        _container.Add(new Label(listLabel));
        _container.Add(controlButtonsContainer);
        _container.Add(_listView);

        _listView.makeItem = () =>
        {
            var item = new TView();
            item.AddToClassList(itemCssClass);
            return item;
        };
        _listView.itemsSource = _models;
        _listView.bindItem += OnBindItemInternal;
    }

    private Button GetButtonWithLabel(string label, Action clickEvent)
    {
        return new Button(clickEvent)
        {
            text = label
        };
    }

    public void Build(VisualElement root)
    {
        root.Add(_container);
    }

    private void OnAddItem()
    {
        _models.Add(new TModel());
        _listView.RefreshItems();
    }

    private void OnRemoveItem()
    {
        foreach (var index in _listView.selectedIndices)
        {
            _models.RemoveAt(index);
        }

        _listView.RefreshItems();
    }

    private void OnBindItemInternal(VisualElement view, int index)
    {
        view.userData = index;
        OnBindItem(view as TView, index);
    }

    protected abstract void OnBindItem(TView view, int index);
}
