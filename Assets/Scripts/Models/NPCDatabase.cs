using System.Collections.Generic;
using UnityEngine;

namespace Puhinsky.DND.Models
{
    public class NPCDatabase : MonoBehaviour
    {
        [SerializeField] private List<NPCWrapper> _templates;

        public IReadOnlyCollection<NPCWrapper> Templates => _templates;
    }
}
