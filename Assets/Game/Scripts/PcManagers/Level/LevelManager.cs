using System.Collections.Generic;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.Root;
using Game.Scripts.Tick;
using UnityEngine;

namespace Game.Scripts.PcManagers.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform _levelParent;
        [SerializeField] private List<Level> _levelPrefabs;
        [SerializeField] private LoadingWindow _loadingWindow;
        
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
            G.Get<TickManager>().Pause();
            _loadingWindow.Show(() =>
            {
                G.Get<TickManager>().Unpause();
            });
            
            G.Get<PlayerManager>().Reset();
            
            if (_currentLevel != null)
            {
                Destroy(_currentLevel.gameObject);
                _currentLevel = null;
            }
            
            Instantiate(_levelPrefabs[index], _levelParent);
        }
        
        public void RestartCurrentLevel()
        {
            _currentLevel.RestartLevel();
        }
    }
}