using System;
using Game.Scripts.PcManagers.Player.Item;

namespace Game.Scripts.PcManagers.Pacient
{
    public class ZombiePatientBehaviour : APatientBehaviour
    {
        public override void UseItem(UseItem useItem, Action callback)
        {
            if (useItem.Uid == "Pistol")
            {
                //play die animation
            }
        }
    }
}