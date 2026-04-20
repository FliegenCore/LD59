using System;
using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.PcManagers.Level
{
    public class LoadingWindow : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private AudioSource _loadingSound;

        public void Show(Action callback)
        {
            gameObject.SetActive(true);
            _loadingSound.Play();
            var track = _skeletonAnimation.AnimationState.SetAnimation(0, "loading", false);

            track.Complete += (_) =>
            {
                _skeletonAnimation.AnimationState.SetEmptyAnimation(0,0);
                gameObject.SetActive(false);
                callback?.Invoke();
            };
        }
    }
}