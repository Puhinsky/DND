using Puhinsky.DND.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Puhinsky.DND.UI
{
    public class PlayerListController : ListController<PlayerModel, PlayerUiView>
    {
        private readonly List<PlayerView> _gameViews = new();

        private const string itemCssClass = "player";

        public PlayerListController(string listLabel) : base(listLabel, itemCssClass)
        {
            style.flexGrow = 1;
            ListView.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        }

        protected override void OnModelAdded()
        {
            var view = PlayerView.Instance();
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
