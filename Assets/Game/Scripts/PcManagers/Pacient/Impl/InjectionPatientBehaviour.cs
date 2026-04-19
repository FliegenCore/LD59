using Game.Scripts.PcManagers.Player.Item;

namespace Game.Scripts.PcManagers.Pacient
{
    public class InjectionPatientBehaviour : APatientBehaviour
    {
        public override void UseItem(UseItem useItem)
        {
            string uid = useItem.Uid;

            if (uid == "Injection")
            {
                _view.BoxCollider2D.enabled = false;
            }
            else
            {
                //взорваться или еще что
                //вызвать проигрыш и перезапуск уровня
            }
        }
    }
}