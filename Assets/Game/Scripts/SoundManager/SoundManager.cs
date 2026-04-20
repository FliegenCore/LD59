using UnityEngine;
using UnityEngine.Audio;

namespace Game.Scripts.SoundManager
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _volumeParameterName = "Volume";
        [SerializeField] private int _stepsCount = 5;                        

        private float[] _volumeStepsDB; 
        private int _currentStep;       

        private void Start()
        {
            if (_audioMixer == null)
            {
                Debug.LogError("AudioMixer не назначен в SoundManager!", this);
                return;
            }

            _volumeStepsDB = new float[_stepsCount];
            for (int i = 0; i < _stepsCount; i++)
            {
                float linear = i / (float)(_stepsCount - 1);
                float dB = linear > 0.01f ? Mathf.Log10(linear) * 20f : -80f;
                _volumeStepsDB[i] = dB;
            }

            ApplyCurrentVolume();
        }

        public void IncreaseVolume()
        {
            if (_currentStep < _stepsCount - 1)
            {
                _currentStep++;
                ApplyCurrentVolume();
            }
        }

        public void DecreaseVolume()
        {
            if (_currentStep > 0)
            {
                _currentStep--;
                ApplyCurrentVolume();
            }
        }

        private void ApplyCurrentVolume()
        {
            float targetDB = _volumeStepsDB[_currentStep];
            _audioMixer.SetFloat(_volumeParameterName, targetDB);
            Debug.Log($"Громкость изменена: шаг {_currentStep + 1}/{_stepsCount} ({targetDB:F2} dB)");
        }
    }
}