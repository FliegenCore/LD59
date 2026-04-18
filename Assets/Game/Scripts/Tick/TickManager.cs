using System;
using UnityEngine;

namespace Game.Scripts.Tick
{
    public class TickManager : MonoBehaviour
    {
        public event Action<float> OnTick;
        
        [SerializeField] private AudioClip[] _tickSound;
        [SerializeField] private AudioSource _audioSource;
        private float _bpm = 124f;
    
        private float _nextTickTime;
        private float _tickInterval;
        private float _lastTickTime;

        private int _currentTickSound;
        private bool _isInitialized;
        
        public void Initialize()
        {
            _audioSource = GetComponent<AudioSource>();
            _tickInterval = 60f / _bpm;
            _nextTickTime = Time.time;
            _isInitialized = true;
        }
    
        private void Update()
        {
            if (!_isInitialized)
                return;
            
            while (Time.time >= _nextTickTime)
            {
                PlayTick();
                _nextTickTime += _tickInterval;
            
                if (_nextTickTime < Time.time)
                    _nextTickTime = Time.time + _tickInterval;
            }
        
            CheckInput();
        }
    
        private void PlayTick()
        {
            if (_currentTickSound >= _tickSound.Length)
            {
                _currentTickSound = 0;
            }
            
            _audioSource.PlayOneShot(_tickSound[_currentTickSound]);
            _currentTickSound++;
            _lastTickTime = Time.time;
        }
    
        private void CheckInput()
        {
            float error = Mathf.Abs(Time.time - _lastTickTime);
            
            OnTick?.Invoke(error);
        }
    }
}