using Game.Scripts.PcManagers.Pacient;
using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;
using Game.Scripts.Tick;
using UnityEngine.UI;

namespace Game.Scripts.PcManagers.Player.Impl
{
    public class PistolCombo : AComboBehaviour
    {
        private readonly PlayerView _playerView;
        
        public PistolCombo(PlayerView playerView)
        {
            _playerView = playerView;
        }
        
        public override void Play()
        {
            if (G.Get<Raycaster>().TryGetPatient(out var patient, _playerView.Origin, 4f))
            {
                if (patient is IZombie zombie)
                {
                    if (zombie.IsAlive())
                    {
                        _playerView.PlayAnimation("attack", false);
                        patient.UseItem(new UseItem{Uid = "Pistol"}, () =>
                        {
                    
                        });
                    }
                }
            }
            else
            {
                _playerView.PlayAnimation("attackAIR", false);
            }
        }

        public override void Stop()
        {
            
        }

        public override bool CanPlay()
        {
            return true;
        }

        public override void Reset()
        {
            
        }
    }
}