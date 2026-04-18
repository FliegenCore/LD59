using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PcManagers.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<LevelConfig> _levelConfings;
        private int _currentLevel;
        
        public void Initialize()
        {
            
        }

        public void NextLevel()
        {
            _currentLevel++;
            CreateLevel();
        }
        
        public void CreateLevel()
        {
            
        }
        
        public void RestartCurrentLevel()
        {
            //initialize all characters
        }
    }
}