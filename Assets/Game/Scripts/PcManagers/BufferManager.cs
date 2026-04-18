using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PcManagers
{
    public class BufferManager
    {
        public event Action OnBufferFull;
        public event Action<List<KeyCode>> OnBufferChanged;
        private List<KeyCode> _bufferKeys = new List<KeyCode>();

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
        
        public void AddKey(KeyCode key)
        {
            _bufferKeys.Add(key);
            OnBufferChanged?.Invoke(_bufferKeys);
            
            if(_bufferKeys.Count == 4)
                OnBufferFull?.Invoke();
        }
        
        public void Clear()
        {
            _bufferKeys.Clear();
            OnBufferChanged?.Invoke(_bufferKeys);
        }
    }
}