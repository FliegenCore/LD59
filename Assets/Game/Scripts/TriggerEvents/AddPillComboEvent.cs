using System;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.TriggerEvents
{
    public class AddPillComboEvent : MonoBehaviour
    {
        [SerializeField] private ComboConfig _pillCombo;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerView playerView))
            {
                G.Get<PlayerManager>().AddCombo(_pillCombo);
                gameObject.SetActive(false);
            }
        }
    }
}