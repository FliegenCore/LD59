using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.Root;
using Game.Scripts.Tick;
using GameAnalyticsSDK;
using UnityEngine;

namespace Game.Scripts.PcManagers.Pacient
{
    public class BossPatientBehaviour : APatientBehaviour, IZombie, IInitializable
    {
        [SerializeField] private AudioSource _dieSound;
        private bool _isAlive;
        public bool IsAlive() => _isAlive;
        
        
        public void Initialize()
        {
            _isAlive = true;
        }

        public override void UseItem(UseItem useItem, Action callback)
        {
            if (useItem.Uid == "Pistol")
            {
                G.Get<TickManager>().Pause();
                _view.BoxCollider2D.enabled = false;
                _view.PlayAnimation("die", false);
                StartCoroutine(WaitDieSound());
                BossDie();
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
        }
        
        private void BossDie()
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "complete game");
            //show end
            //show GG die
            //show end cutscene
        }
    }
}