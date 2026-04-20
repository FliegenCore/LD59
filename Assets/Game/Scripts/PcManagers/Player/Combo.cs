using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player
{
    public class Combo
    {
        public readonly AComboBehaviour ComboBehaviour;
        private bool _isPlay;
        private List<KeyCode> comboKeys = new();
        public string Uid;
        public bool IsPlay => _isPlay;
        public List<KeyCode> ComboKeys => comboKeys;

        public Combo(ComboConfig config, AComboBehaviour comboBehaviour)
        {
            comboKeys = config.ComboKeys;
            Uid = config.Uid;
            ComboBehaviour = comboBehaviour;

            ComboBehaviour.OnComplete += Stop;
        }
        
        public void Play()
        {
            ComboBehaviour.Play();
            _isPlay = true;
        }

        public void Stop()
        {
            ComboBehaviour.Stop();
            _isPlay = false;
        }
        
        public bool CanPlay()
        {
            return ComboBehaviour.CanPlay();
        }

        public void Reset()
        {
            ComboBehaviour.Reset();
        }
    }
}