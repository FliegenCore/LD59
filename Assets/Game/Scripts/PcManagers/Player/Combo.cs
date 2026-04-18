using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player
{
    public class Combo
    {
        private readonly AComboBehaviour _comboBehaviour;
        private bool _isPlay;
        private List<KeyCode> comboKeys = new();
        public string Uid;
        public bool IsPlay => _isPlay;
        public List<KeyCode> ComboKeys => comboKeys;

        public Combo(ComboConfig config, AComboBehaviour comboBehaviour)
        {
            comboKeys = config.ComboKeys;
            Uid = config.Uid;
            _comboBehaviour = comboBehaviour;

            _comboBehaviour.OnComplete += Stop;
        }
        
        public void Play()
        {
            _comboBehaviour.Play();
            _isPlay = true;
        }

        public void Stop()
        {
            _comboBehaviour.Stop();
            _isPlay = false;
        }
    }
}