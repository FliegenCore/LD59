using System;
using System.Collections;
using Game.Scripts.PcManagers.Level;
using Game.Scripts.PcManagers.Player.Item;
using Game.Scripts.Root;
using Game.Scripts.Tick;
using UnityEngine;

namespace Game.Scripts.PcManagers.Pacient
{
    public class PillPatientBehaviour: APatientBehaviour
    {
        [SerializeField] private AudioSource _dieSound;

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
                StartCoroutine(PlayDieSound());
                _view.PlayAnimation("bad", false, () =>
                {
                    G.Get<LevelManager>().RestartCurrentLevel();
                });
            }
        }

        private IEnumerator PlayDieSound()
        {
            yield return new WaitForSeconds(1.1f);
            _dieSound.Play();
        }
    }
}