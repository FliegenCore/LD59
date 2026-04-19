using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PcManagers.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform _levelParent;
        [SerializeField] private List<Level> _levelPrefabs;

        private Level _currentLevel;
        private int _currentLevelIndex;
        
        public void Initialize()
        {
            
        }

        public void NextLevel()
        {
            _currentLevelIndex++;
            CreateLevel(_currentLevelIndex);
        }
        
        public void CreateLevel(int index)
        {
            if (_currentLevel != null)
            {
                Destroy(_currentLevel.gameObject);
                _currentLevel = null;
            }
            
            Instantiate(_levelPrefabs[index], _levelParent);
        }
        
        public void RestartCurrentLevel()
        {
            //initialize all characters
        }
    }
}