using Game.Scripts.PcManagers.Player.Item;
using UnityEngine;

namespace Game.Scripts.PcManagers.Pacient
{
    public abstract class APatientBehaviour : MonoBehaviour
    {
        [SerializeField] protected PatientView _view;
        
        public abstract void UseItem(UseItem useItem);

        public virtual void Reset()
        {
            _view.BoxCollider2D.enabled = true;
            //set base animation;
        }
    }
}