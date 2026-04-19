using Game.Scripts.PcManagers.Player.Item;
using UnityEngine;

namespace Game.Scripts.PcManagers.Pacient
{
    public class PillPatientBehaviour: APatientBehaviour
    {
        public override void UseItem(UseItem useItem)
        {
            string uid = useItem.Uid;

            if (uid == "Pill")
            {
                _view.BoxCollider2D.enabled = false;
                Debug.Log("СПАСИБО ЗА ТАБЛЕКУ");
            }
            else
            {
                //взорваться или еще что 
            }
        }

        public override void Reset()
        {
            _view.BoxCollider2D.enabled = true;
            //set base animation;
        }
    }
}