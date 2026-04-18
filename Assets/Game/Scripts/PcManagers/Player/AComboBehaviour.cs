using System;

namespace Game.Scripts.PcManagers.Player
{
    public abstract class AComboBehaviour
    {
        public Action OnComplete;
        
        public abstract void Play();
        public abstract void Stop();
    }
}