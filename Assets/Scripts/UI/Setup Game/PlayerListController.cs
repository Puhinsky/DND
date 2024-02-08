using Puhinsky.DND.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    [UxmlElement]
    public partial class PlayerListController : ListController<PlayerModel, PlayerUiView>
    {
        private readonly List<CharacterView> _gameViews = new();

        private const string _itemCssClass = "player";
        private const string _listCssClass = "player-list";

        public PlayerListController() : base("Player List", _itemCssClass)
        {
            Init();
        }

        public PlayerListController(string listLabel) : base(listLabel, _itemCssClass)
        {
            Init();
        }

        private void Init()
        {
            style.flexGrow = 1;
            AddToClassList(_listCssClass);
            ListView.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        }

        protected override void OnModelAdded()
        {
            var view = CharacterView.Instance();
            _gameViews.Add(view);
            view.BindModel(Models.Last());
        }

        protected override void OnModelRemoved(int index)
        {
            _gameViews[index].Destroy();
            _gameViews.RemoveAt(index);
        }

        protected override void OnBindItem(PlayerUiView view, int index)
        {
            view.BindModel(Models[index]);
        }

        protected override void OnUnbindItem(PlayerUiView view, int index)
        {
            view.UnbindModel();
        }
    }
}
