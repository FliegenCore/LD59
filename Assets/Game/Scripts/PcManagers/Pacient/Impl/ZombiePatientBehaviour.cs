using System;
using Game.Scripts.PcManagers.Level;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.Root;
using Game.Scripts.Tick;

namespace Game.Scripts.PcManagers.Pacient
{
    public class ZombiePatientBehaviour : APatientBehaviour, IInitializable
    {
        public void Initialize()
        {
            G.Get<PlayerManager>().SubscribeOnCompleteCurrentAction(TryKillPlayer);
        }
        
        public override void UseItem(UseItem useItem, Action callback)
        {
            if (useItem.Uid == "Pistol")
            {
                G.Get<TickManager>().Pause();
                _view.BoxCollider2D.enabled = false;
                _view.PlayAnimation("die", false, () =>
                {
                    G.Get<TickManager>().Unpause();
                    
                });
            }
        }

        public override void Reset()
        {
            base.Reset();
            G.Get<PlayerManager>().UnsubscribeOnCompleteCurrentAction(TryKillPlayer);
        }

        private void TryKillPlayer()
        {
            if (G.Get<Raycaster>().TryGetPlayer(out _, _view.Origin))
            {
                G.Get<TickManager>().Pause();
                
                _view.PlayAnimation("attack", false, () =>
                {
                    G.Get<PlayerManager>().PlayerDie();
                });
            }
        }
    }
}