using System;
using Game.Scripts.PcManagers.Level;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.Root;
using Game.Scripts.Tick;

namespace Game.Scripts.PcManagers.Pacient
{
    public class ZombiePatientBehaviour : APatientBehaviour, IInitializable, IZombie
    {
        private bool _isAlive;
        
        public void Initialize()
        {
            G.Get<PlayerManager>().SubscribeOnCompleteCurrentAction(TryKillPlayer);
            _isAlive = true;
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
                    OnDie();
                });
            }
        }

        public override void Reset()
        {
            _isAlive = true;
            base.Reset();
            G.Get<PlayerManager>().UnsubscribeOnCompleteCurrentAction(TryKillPlayer);
        }

        private void OnDie()
        {
            _isAlive = false;
            G.Get<PlayerManager>().UnsubscribeOnCompleteCurrentAction(TryKillPlayer);
            _view.BoxCollider2D.enabled = false;
        }

        private void TryKillPlayer()
        {
            if (G.Get<Raycaster>().TryGetPlayer(out _, _view.Origin))
            {
                G.Get<TickManager>().Pause();
                _view.PlayAnimation("attack", false);
                G.Get<PlayerManager>().PlayerDie();
            }
        }

        public bool IsAlive()
        {
            return _isAlive;
        }
    }
}