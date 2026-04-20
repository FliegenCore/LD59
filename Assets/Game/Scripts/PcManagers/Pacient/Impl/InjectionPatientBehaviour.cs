using System;
using Game.Scripts.PcManagers.Level;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.Root;
using Game.Scripts.Tick;

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
                G.Get<TickManager>().Pause();
                _view.PlayAnimation("bad", false, () =>
                {
                    G.Get<LevelManager>().RestartCurrentLevel();
                });
                _view.BoxCollider2D.enabled = false;
                //взорваться или еще что
                //вызвать проигрыш и перезапуск уровня
            }
        }
    }
}