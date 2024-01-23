using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ListController<TModel, TView> : VisualElement where TModel : class, new() where TView : VisualElement, new()
{
    private readonly VisualElement _container = new();
    private readonly List<TModel> _models = new();

    public IReadOnlyList<TModel> Models => _models;
    protected ListView ListView { get; private set; } = new();

    private readonly string _itemCssClass;

    private const string _addItemLabel = "+";
    private const string _removeItemLabel = "-";
    private const string _buttonContainerClass = "list-button-container";

    public ListController(string listLabel, string itemCssClass)
    {
        var controlButtonsContainer = new VisualElement();
        controlButtonsContainer.style.flexDirection = FlexDirection.Row;
        controlButtonsContainer.Add(GetButtonWithLabel(_addItemLabel, OnAddItem));
        controlButtonsContainer.Add(GetButtonWithLabel(_removeItemLabel, OnRemoveItem));
        controlButtonsContainer.AddToClassList(_buttonContainerClass);

        _container.Add(new Label(listLabel));
        _container.Add(controlButtonsContainer);
        _container.Add(ListView);
        _container.style.flexGrow = 1;

        _itemCssClass = itemCssClass;
        ListView.itemsSource = _models;
        ListView.makeItem += OnMakeItemInternal;
        ListView.destroyItem += OnDestroyItemInternal;
        ListView.bindItem += OnBindItemInternal;
        ListView.unbindItem += OnUnbindItemInternal;
        Add(_container);
    }

    private Button GetButtonWithLabel(string label, Action clickEvent)
    {
        return new Button(clickEvent)
        {
            text = label
        };
    }

    private void OnAddItem()
    {
        _models.Add(new TModel());
        ListView.RefreshItems();
        OnModelAdded();
    }

    private void OnRemoveItem()
    {
        var index = ListView.selectedIndex;

        if (index < 0)
            return;

        _models.RemoveAt(index);
        OnModelRemoved(index);
        ListView.RefreshItems();
    }

    private VisualElement OnMakeItemInternal()
    {
        var item = new TView();
        item.AddToClassList(_itemCssClass);
        OnMakeItem();

        return item;
    }

    private void OnDestroyItemInternal(VisualElement item)
    {
        OnDestroyItem();
    }

    private void OnBindItemInternal(VisualElement view, int index)
    {
        OnBindItem(view as TView, index);
    }

    private void OnUnbindItemInternal(VisualElement view, int index)
    {
        OnUnbindItem(view as TView, index);
    }

    protected virtual void OnMakeItem() { }
    protected virtual void OnDestroyItem() { }
    protected virtual void OnBindItem(TView view, int index) { }
    protected virtual void OnUnbindItem(TView view, int index) { }

    protected virtual void OnModelAdded() { }
    protected virtual void OnModelRemoved(int index) { }
}
