using Puhinsky.DND.Models;
using System;
using UnityEngine;

namespace Puhinsky.DND.UI
{
    public class PlayerSelectionHandler : MonoBehaviour
    {
        public Action<ICharacter> PlayerSelected { get; set; }
        public Action<ICharacter> PlayerDeselected { get; set; }

        private ICharacter _selectedPlayer;

        public void OnPlayerMouseDown(ICharacter model)
        {
            if (_selectedPlayer != model)
            {
                PlayerSelected?.Invoke(model);
            }
        }

        public void OnPlayerMouseUp(ICharacter model)
        {
            if (_selectedPlayer == model)
            {
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
