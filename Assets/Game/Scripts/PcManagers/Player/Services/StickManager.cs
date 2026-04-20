using UnityEngine;

namespace Game.Scripts.PcManagers.Player
{
    public class StickManager : MonoBehaviour
    {
        [SerializeField] private StickComboView[] _stickers;

        public void Initialize()
        {
            foreach (var sticker in _stickers)
            {
                sticker.Initialize();
            }
        }
    }
}