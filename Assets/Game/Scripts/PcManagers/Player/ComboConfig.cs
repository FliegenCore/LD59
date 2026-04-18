using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player
{
    [CreateAssetMenu(fileName = "comboConfig", menuName = "Game/Player/ComboConfig")]
    public class ComboConfig : ScriptableObject
    {
        public string Uid;
        public List<KeyCode> ComboKeys;
    }
}