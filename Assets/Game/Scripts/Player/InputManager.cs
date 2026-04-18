using System;
using Game.Scripts.Tick;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class InputManager : MonoBehaviour
    {
        public event Action OnPerfect;
        public event Action OnError;
        public event Action<KeyCode> OnInput;
        
        [SerializeField] private TickManager _tickManager;

        public void Initialize()
        {
            _tickManager.OnTick += CheckInput;
        }

        private void CheckInput(float error)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnInput?.Invoke(KeyCode.LeftArrow);
                CheckTiming(error);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnInput?.Invoke(KeyCode.RightArrow);
                CheckTiming(error);
            }
        }

        private void CheckTiming(float error)
        {
            if (error < 0.2f)
            {
                Debug.Log("Perfect");
                OnPerfect?.Invoke();
                
            }
            else
            {
                Debug.Log("Error");
                OnError?.Invoke();
            }
        }
    }
}