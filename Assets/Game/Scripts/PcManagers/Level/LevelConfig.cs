using System.Collections.Generic;
using Game.Scripts.PcManagers.Player;
using UnityEngine;

namespace Game.Scripts.PcManagers.Level
{
    [CreateAssetMenu(fileName =  "LevelConfig", menuName = "Game/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public List<ComboConfig> NewCombos;
    }
}