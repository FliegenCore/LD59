using System;
using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.Root;
using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        public Transform Origin;
        private Spine.AnimationState _animationState;

        private void Awake()
        {
            _skeletonAnimation.Initialize(true);
            _animationState = _skeletonAnimation.AnimationState;
        }
        
        public void PlayAnimation(string animationName, bool isLoop, Action callback = null)
        {
            var track = _animationState.SetAnimation(0, animationName, isLoop);

            
            
            if (!isLoop)
            {
                track.Complete += _ =>
                {
                    if (callback != null)
                    {
                        callback?.Invoke();
                    }
                    ExitOnIdleAnimation();
                };
            }
        }

        private void ExitOnIdleAnimation()
        {
            _animationState.SetAnimation(0, "idle", true);
        }
    }
}