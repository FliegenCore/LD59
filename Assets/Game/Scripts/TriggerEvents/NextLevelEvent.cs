using Game.Scripts.PcManagers.Level;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;
using UnityEngine;

namespace Game.Scripts.TriggerEvents
{
    public class NextLevelEvent : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerView playerView))
            {
                gameObject.SetActive(false);
                
                //first show loading on screen
                G.Get<LevelManager>().NextLevel();
            }
        }
    }
}