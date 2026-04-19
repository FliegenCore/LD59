using System;
using Game.Scripts.Tick;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class InputManager : MonoBehaviour
    {
        public event Action<KeyCode> OnPerfect;
        public event Action OnError;
        public event Action<KeyCode> OnInput; 

        private int _pressCount;       
        private float _firstError;    
        private bool _wasClick;
        
        
        [SerializeField] private TickManager _tickManager;

        public void Initialize()
        {
            _tickManager.OnUpdate += CheckInput;
            _tickManager.OnInputWindowClosed += OnInputWindowClosed;
        }

        private void ResetForNewTick()
        {
            _pressCount = 0;
            _firstError = 0f;
        }

        private void Update()
        {
            _tickManager.RegisterInput();
        }

        private void CheckInput(float error)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                _wasClick = true;
                OnInput?.Invoke(KeyCode.LeftArrow); 
                OnClick(error, KeyCode.LeftArrow);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                _wasClick = true;
                OnInput?.Invoke(KeyCode.RightArrow); 
                OnClick(error, KeyCode.RightArrow);
            }
        }

        private void OnClick(float error, KeyCode key)
        {
            _pressCount++;
            
            if (_pressCount > 1)
            {
                OnError?.Invoke();
                ResetForNewTick();
                return;
            }
            
            if (error < 0.2f)
                OnPerfect?.Invoke(key);
            else
            {
                OnError?.Invoke();
                ResetForNewTick();
            }
        }
        
        private void OnInputWindowClosed()
        {
            if (!_wasClick)
            {
                OnError?.Invoke();
                ResetForNewTick();
                return;
            }
            
            _wasClick = false;
            ResetForNewTick(); 
        }
    }
}