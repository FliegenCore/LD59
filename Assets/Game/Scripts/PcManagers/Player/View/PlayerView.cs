using System;
using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        
        private Spine.AnimationState _animationState;

        private void Awake()
        {
            _skeletonAnimation.Initialize(true);
            _animationState = _skeletonAnimation.AnimationState;
        }

        public void PlayAnimation()
        {
            
        }
    }
}