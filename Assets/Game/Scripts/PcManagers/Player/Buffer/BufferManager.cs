using System;
using System.Collections.Generic;
using Game.Scripts.Player;
using Game.Scripts.Root;
using UnityEngine;

namespace Game.Scripts.PcManagers
{
    public class BufferManager
    {
        public event Func<bool> OnBufferFull;
        public event Action<List<KeyCode>> OnBufferChanged;
        private List<KeyCode> _bufferKeys = new List<KeyCode>();
        private BufferView _bufferView;

        public void Initialize()
        {
            G.Get<InputManager>().OnPerfect += AddKey;
            G.Get<InputManager>().OnError += Clear;
            _bufferView = G.Get<BufferView>();
            _bufferView.Initialize();
        }
        
        public bool HasCombo(List<KeyCode> keys)
        {
            if(_bufferKeys.Count != keys.Count)
            {
                return false;
            }

            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] != _bufferKeys[i])
                    return false;
            }

            return true;
        }
        
        private void AddKey(KeyCode key)
        {
            _bufferKeys.Add(key);
            _bufferView.ShowValue(_bufferKeys.Count, key);
            OnBufferChanged?.Invoke(_bufferKeys);

            if (_bufferKeys.Count == 4)
            {
                bool result = OnBufferFull != null && OnBufferFull();

                if (!result)
                    Clear();
                else
                    ClearWithGoodAnimation();
            }
        }

        private void ClearWithGoodAnimation()
        {
            if (_bufferKeys.Count <= 0)
                return;
            
            _bufferKeys.Clear();
            OnBufferChanged?.Invoke(_bufferKeys);
            _bufferView.ClearGoodAnimation();
        }
        
        private void Clear()
        {
            if (_bufferKeys.Count <= 0)
                return;
            
            _bufferKeys.Clear();
            OnBufferChanged?.Invoke(_bufferKeys);
            _bufferView.Clear();
        }
    }
}