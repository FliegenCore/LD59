using System;
using Game.Scripts.PcManagers.Level;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.Root;
using Game.Scripts.Tick;
using UnityEngine;

namespace Game.Scripts.PcManagers.Pacient
{
    public class PillPatientBehaviour: APatientBehaviour
    {
        public override void UseItem(UseItem useItem, Action callback)
        {
            string uid = useItem.Uid;

            if (uid == "Pill")
            {
                GoodResult();
            }
            else
            {
                G.Get<TickManager>().Pause();
                _view.PlayAnimation("bad", false, () =>
                {
                    G.Get<LevelManager>().RestartCurrentLevel();
                });
                
                //взорваться или еще что 
            }
        }
    }
}