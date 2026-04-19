using System;
using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.PcManagers.Level
{
    public class LoadingWindow : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        public void Show(Action callback)
        {
            gameObject.SetActive(true);
            var track = _skeletonAnimation.AnimationState.SetAnimation(0, "loading", false);

            track.Complete += (_) =>
            {
                gameObject.SetActive(false);
                callback?.Invoke();
            };
        }
    }
}