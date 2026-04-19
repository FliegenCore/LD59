using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;

namespace Game.Scripts.PcManagers.Player.Impl
{
    public class InjectionCombo : AComboBehaviour
    {
        private readonly PlayerView _playerView;
        
        public InjectionCombo(PlayerView playerView)
        {
            _playerView = playerView;
        }
        
        public override void Play()
        {
            _playerView.PlayAnimation("attack1", false);
        }

        public override void Stop()
        {
            
        }

        public override bool CanPlay()
        {
            return G.Get<Raycaster>().TryGetPatient(out var patient, _playerView.transform);
        }

        public override void Reset()
        {
            
        }
    }
}