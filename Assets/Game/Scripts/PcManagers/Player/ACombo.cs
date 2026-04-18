using UnityEngine;

namespace Game.Scripts.PcManagers.Player
{
    public abstract class ACombo : MonoBehaviour
    {
        public bool IsPlay { get; set; }
        public abstract string Uid { get; set; }
        public abstract void Play();
        public abstract void Stop();
    }
}