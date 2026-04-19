using System;
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
                //взорваться или еще что 
            }
        }
    }
}