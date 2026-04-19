using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player.Impl
{
    public class PillCombo : AComboBehaviour
    {
        private readonly PlayerView _playerView;
        private readonly Raycaster _raycaster;
        
        public PillCombo(PlayerView playerView)
        {
            _playerView = playerView;
            _raycaster = G.Get<Raycaster>();
        }
        
        public override void Play()
        {
            if (_raycaster.TryGetPatient(out var patient, _playerView.Origin))
            {
                //play animation
                _playerView.PlayAnimation("pills", false);
                patient.UseItem(new UseItem{Uid = "Pill"}, () =>
                {
                    
                });   
            }
            else
            {
                //кинуть таблетку
            }
            //do raycast
            //health npc
        }

        public override void Stop()
        {
            
        }

        public override bool CanPlay()
        {
            return _raycaster.TryGetPatient(out var patient, _playerView.transform);
        }

        public override void Reset()
        {
            
        }
    }
}