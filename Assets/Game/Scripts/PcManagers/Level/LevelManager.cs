using System.Collections.Generic;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.Root;
using Game.Scripts.Tick;
using GameAnalyticsSDK;
using TMPro;
using UnityEngine;

namespace Game.Scripts.PcManagers.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform _levelParent;
        [SerializeField] private List<Level> _levelPrefabs;
        [SerializeField] private LoadingWindow _loadingWindow;
        [SerializeField] private TMP_Text _currentLevelText;
        
        private Level _currentLevel;
        private int _currentLevelIndex;
        
        public void Initialize()
        {
        }

        public void NextLevel()
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"{_currentLevelIndex}");
            _currentLevelIndex++;
            CreateLevel(_currentLevelIndex);
        }
        
        public void CreateLevel(int index)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"{index}");
            
            _currentLevelText.text = $"{index + 1}/{_levelPrefabs.Count}";
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
            
            _currentLevel = Instantiate(_levelPrefabs[index], _levelParent);
            _currentLevel.Initialize();
            foreach (var newCombo in _currentLevel.NewCombos)
            {
                G.Get<PlayerManager>().AddCombo(newCombo);
            }
        }
        
        public void RestartCurrentLevel()
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"{_currentLevelIndex}");
            G.Get<TickManager>().Pause();
            _loadingWindow.Show(() =>
            {
                G.Get<TickManager>().Unpause();
            });
            
            _currentLevel.RestartLevel();
        }
    }
}