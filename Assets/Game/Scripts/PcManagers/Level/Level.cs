using System.Collections.Generic;
using Game.Scripts.PcManagers.Pacient;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.Root;
using UnityEngine;

namespace Game.Scripts.PcManagers.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private APatientBehaviour[] _patients;
        public List<ComboConfig> NewCombos;
        
        public void RestartLevel()
        {
            G.Get<PlayerManager>().Reset();
            foreach (var patient in _patients)
            {
                patient.Reset();
            }
        }
        
        
    }
}