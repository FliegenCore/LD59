using System;
using Spine.Unity;
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
        [SerializeField] private AudioSource _pauseAudioSource;
        [SerializeField] private float _bpm = 124f;
        [SerializeField] private float _inputWindowDuration = 0.2f;
        [SerializeField] private SkeletonAnimation _tickAnimation;

        private float _nextTickTime;
        private float _tickInterval;
        private float _lastTickTime;
        private float _nextWindowCloseTime;
        private bool _windowOpen;
        private int _currentTickSound;
        private bool _isInitialized;

        private bool _isPaused;

        public void Initialize()
        {
            _audioSource = GetComponent<AudioSource>();
            _tickInterval = 60f / _bpm;
            _nextTickTime = Time.time;
            _isInitialized = true;
            _windowOpen = false;
        }

        private void Update()
        {
            if (!_isInitialized || _isPaused) return;

            while (Time.time >= _nextTickTime)
            {
                if (_windowOpen)
                {
                    _windowOpen = false;
                    OnInputWindowClosed?.Invoke();
                }
                
                PlayTick();
                _nextTickTime += _tickInterval;
                if (_nextTickTime < Time.time)
                    _nextTickTime = Time.time + _tickInterval;
            }

            if (_windowOpen && Time.time >= _nextWindowCloseTime)
            {
                _windowOpen = false;
                OnInputWindowClosed?.Invoke();
            }
        }

        public void Pause()
        {
            if(!_isPaused)
                _pauseAudioSource.Play();
            
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }

        private void PlayTick()
        {
            if (_currentTickSound >= _tickSound.Length)
                _currentTickSound = 0;

            var trackEntry = _tickAnimation.AnimationState.SetAnimation(0, "tick", false);

            trackEntry.Complete += (_) =>
            {
                _tickAnimation.AnimationState.SetAnimation(0, "idle", false);
            };
            
            _audioSource.PlayOneShot(_tickSound[_currentTickSound]);
            _currentTickSound++;
            _lastTickTime = Time.time;
            _nextWindowCloseTime = _lastTickTime + _inputWindowDuration;
            _windowOpen = true;
            OnTick?.Invoke();
        }

        public void RegisterInput()
        {
            if (!_isInitialized || _isPaused) return;
            float error = Mathf.Abs(Time.time - _lastTickTime);
            OnUpdate?.Invoke(error);
        }
    }
}
