using System;
using Game.Scripts.PcManagers.Player.Item;

namespace Game.Scripts.PcManagers.Pacient
{
    public class InjectionPatientBehaviour : APatientBehaviour
    {
        public override void UseItem(UseItem useItem, Action callback)
        {
            string uid = useItem.Uid;

            if (uid == "Injection")
            {
                GoodResult();
            }
            else
            {
                //взорваться или еще что
                //вызвать проигрыш и перезапуск уровня
            }
        }
    }
}