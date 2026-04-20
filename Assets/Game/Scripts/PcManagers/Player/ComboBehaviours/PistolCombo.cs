using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;
using Game.Scripts.Tick;

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
            G.Get<TickManager>().Pause();
            if (G.Get<Raycaster>().TryGetZombie(out var patient, _playerView.Origin))
            {
                _playerView.PlayAnimation("Pistol", false);
                patient.UseItem(new UseItem{Uid = "Pistol"}, () =>
                {
                    
                });   
            }
            else
            {
                _playerView.PlayAnimation("PistolAir", false, () =>
                {
                    G.Get<TickManager>().Unpause();
                });
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