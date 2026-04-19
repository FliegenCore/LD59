using System;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.Root;
using Game.Scripts.Tick;
using UnityEngine;

namespace Game.Scripts.PcManagers.Pacient
{
    public abstract class APatientBehaviour : MonoBehaviour
    {
        [SerializeField] protected PatientView _view;
        
        public abstract void UseItem(UseItem useItem, Action callback);

        public virtual void Reset()
        {
            _view.BoxCollider2D.enabled = true;
            _view.PlayAnimation("idle", true);
        }


        protected void GoodResult()
        {
            G.Get<TickManager>().Pause();
            _view.PlayAnimation("good", false, () =>
            {
                G.Get<TickManager>().Unpause();
            });
            _view.BoxCollider2D.enabled = false;
        }
    }
}