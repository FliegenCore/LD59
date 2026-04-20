using UnityEngine;

namespace Game.Scripts.SoundManager
{
    public class VolumeButton : MonoBehaviour
    {
        [SerializeField] private bool _isIncreaseButton = true; 

        private void OnMouseDown()
        {
            var soundManager = FindObjectOfType<Game.Scripts.SoundManager.SoundManager>();
            if (soundManager == null) return;

            if (_isIncreaseButton)
                soundManager.IncreaseVolume();
            else
                soundManager.DecreaseVolume();
        }
    }
}