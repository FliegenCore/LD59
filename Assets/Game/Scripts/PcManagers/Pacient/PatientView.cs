using System;
using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.PcManagers.Pacient
{
    public class PatientView : MonoBehaviour
    {
        public BoxCollider2D BoxCollider2D;
        
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        private Spine.AnimationState _animationState;
        public Transform Origin;

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
                    callback?.Invoke();
                };
            }
        }
    }
}