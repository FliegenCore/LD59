using System;
using System.Collections;
using Game.Scripts.PcManagers.Level;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.Root;
using Game.Scripts.Tick;
using UnityEngine;

namespace Game.Scripts.PcManagers.Pacient
{
    public class ZombiePatientBehaviour : APatientBehaviour, IInitializable, IZombie
    {
        [SerializeField] private AudioSource _dieSound;
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
                StartCoroutine(WaitDieSound());
                _view.PlayAnimation("die", false, () =>
                {
                    G.Get<TickManager>().Unpause();
                    OnDie();
                });
            }
        }

        private IEnumerator WaitDieSound()
        {
            yield return new WaitForSeconds(0.5f);
            _dieSound.Play();
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