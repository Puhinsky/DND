using Puhinsky.DND.Models;
using System;
using UnityEngine;

namespace Puhinsky.DND.UI
{
    public class PlayerSelectionHandler : MonoBehaviour
    {
        public Action<PlayerModel> PlayerSelected { get; set; }
        public Action<PlayerModel> PlayerDeselected { get; set; }

        private PlayerModel _selectedPlayer;

        public void OnPlayerMouseDown(PlayerModel model)
        {
            if (_selectedPlayer != model)
            {
                PlayerSelected?.Invoke(model);
            }
        }

        public void OnPlayerMouseUp(PlayerModel model)
        {
            if (_selectedPlayer == model) {
                PlayerDeselected?.Invoke(model);
                _selectedPlayer = null;
            }
            else
            {
                _selectedPlayer = model;
            }
        }
    }
}
