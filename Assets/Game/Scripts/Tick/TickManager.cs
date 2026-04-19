using System;
using UnityEngine;

namespace Game.Scripts.Tick
{
    public class TickManager : MonoBehaviour
    {
        public event Action<float> OnUpdate;        
        public event Action OnTick;                    
        public event Action OnInputWindowClosed;      

        [SerializeField] private AudioClip[] _tickSound;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _bpm = 124f;
        [SerializeField] private float _inputWindowDuration = 0.2f; 

        private float _nextTickTime;
        private float _tickInterval;
        private float _lastTickTime;

        private int _currentTickSound;
        private bool _isInitialized;
        private bool _windowClosedForCurrentTick;

        public void Initialize()
        {
            _audioSource = GetComponent<AudioSource>();
            _tickInterval = 60f / _bpm;
            _nextTickTime = Time.time;
            _isInitialized = true;
            _windowClosedForCurrentTick = false;
        }

        private void Update()
        {
            if (!_isInitialized) return;

            while (Time.time >= _nextTickTime)
            {
                PlayTick();
                _nextTickTime += _tickInterval;
                if (_nextTickTime < Time.time)
                    _nextTickTime = Time.time + _tickInterval;
            }

            if (!_windowClosedForCurrentTick && Time.time > _lastTickTime + _inputWindowDuration)
            {
                _windowClosedForCurrentTick = true;
                OnInputWindowClosed?.Invoke();
            }
            else if (_windowClosedForCurrentTick && Time.time < _lastTickTime + _inputWindowDuration)
            {
                _windowClosedForCurrentTick = false;
            }
        }

        private void PlayTick()
        {
            if (_currentTickSound >= _tickSound.Length)
                _currentTickSound = 0;

            _audioSource.PlayOneShot(_tickSound[_currentTickSound]);
            _currentTickSound++;
            _lastTickTime = Time.time;
            OnTick?.Invoke();
        }
      
        public void RegisterInput()
        {
            if (!_isInitialized) return;
            float error = Mathf.Abs(Time.time - _lastTickTime);
            OnUpdate?.Invoke(error);
        }
    }
}