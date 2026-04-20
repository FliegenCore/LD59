using System;
using System.Collections.Generic;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.Root;
using Game.Scripts.Tick;
using GameAnalyticsSDK;

namespace Game.Scripts.PcManagers.Pacient
{
    public class BossPatientBehaviour : APatientBehaviour, IZombie, IInitializable
    {
        private int _currentNeedItem = 0;
        private List<string> _itemNames = new()
        {
            "Pistol",
            "Injection",
            "Pill"
        };

        private bool _isAlive;
        public bool IsAlive() => _isAlive;
        
        public void Initialize()
        {
            G.Get<PlayerManager>().SubscribeOnCompleteCurrentAction(TryKillPlayer);
            _isAlive = true;
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

        public override void UseItem(UseItem useItem, Action callback)
        {
            if (_currentNeedItem > _itemNames.Count - 1)
            {
                if (useItem.Uid == "Pistol")
                {
                    G.Get<TickManager>().Pause();
                    _view.BoxCollider2D.enabled = false;
                    _view.PlayAnimation("die", false, () =>
                    {
                        G.Get<TickManager>().Unpause();
                        BossDie();
                    });
                }
            }
            else
            {
                if (_itemNames[_currentNeedItem] == useItem.Uid)
                {
                    ContinueBoss();
                    _currentNeedItem++;
                }
            }
        }

        private void ContinueBoss()
        {
            
        }
        
        public override void Reset()
        {
            _isAlive = true;
            base.Reset();
            G.Get<PlayerManager>().UnsubscribeOnCompleteCurrentAction(TryKillPlayer);
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