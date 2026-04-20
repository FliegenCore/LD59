using System;
using System.Collections.Generic;
using Game.Scripts.PcManagers.Player.Item;
using GameAnalyticsSDK;

namespace Game.Scripts.PcManagers.Pacient
{
    public class BossPatientBehaviour : APatientBehaviour
    {
        private int _currentNeedItem = 0;
        private List<string> _itemNames = new()
        {
            "Pistol",
            "Injection",
            "Pill"
        };
        
        public override void UseItem(UseItem useItem, Action callback)
        {
            
        }

        private void BossDie()
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "complete game");
            
            //show end cutscene
        }
    }
}