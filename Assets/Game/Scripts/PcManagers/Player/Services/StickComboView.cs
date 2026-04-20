using System.Collections;
using Game.Scripts.Root;
using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player
{
    public class StickComboView : MonoBehaviour
    {
        [SerializeField] private AudioSource _shlep;
        [SerializeField] private string _uid;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        private Spine.AnimationState _animationState;

        public string Uid => _uid;
        
        public void Initialize()
        {
            _skeletonAnimation.Initialize(true);
            _animationState = _skeletonAnimation.AnimationState;

            G.Get<PlayerManager>().OnComboAdd += PlayAnimation;
        }
        
        private void PlayAnimation(string uid)
        {
            if (_uid != uid)
                return;
            
            gameObject.SetActive(true);
            StartCoroutine(WaitSound());
            var track = _animationState.SetAnimation(0, "stick", false);

            track.Complete += _ =>
            {
                ExitOnIdleAnimation();
            };
        }

        private IEnumerator WaitSound()
        {
            yield return new WaitForSeconds(1f);
            _shlep.Play();
        }

        private void ExitOnIdleAnimation()
        {
            _animationState.SetAnimation(0, "idle", true);
        }
    }
}