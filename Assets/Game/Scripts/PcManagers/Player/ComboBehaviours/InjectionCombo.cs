using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;

namespace Game.Scripts.PcManagers.Player.Impl
{
    public class InjectionCombo : AComboBehaviour
    {
        private readonly PlayerView _playerView;
        private readonly Raycaster _raycaster;
        
        public InjectionCombo(PlayerView playerView)
        {
            _playerView = playerView;
            _raycaster = G.Get<Raycaster>();
        }
        
        public override void Play()
        {
            if (_raycaster.TryGetPatient(out var patient, _playerView.Origin))
            {
                _playerView.PlayAnimation("ukol", false);
                patient.UseItem(new UseItem{Uid = "Injection"}, () =>
                {
                    
                });   
            }
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