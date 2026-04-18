using Game.Scripts.Player;
using Game.Scripts.Root;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Game.Scripts
{
    public class HandsManager : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        
        private Spine.AnimationState _animationState;
        private InputManager _inputManager;

        public void Initialize()
        {
            _inputManager = G.Get<InputManager>();
            _animationState = _skeletonAnimation.AnimationState;

            _animationState.SetAnimation(0, "right_idle", true);
            _animationState.SetAnimation(1, "left_idle", true);
            _inputManager.OnInput += OnInput;
        }

        private void OnInput(KeyCode keyCode)
        {
            if (keyCode == KeyCode.LeftArrow || keyCode == KeyCode.A)
            {
                var track = _animationState.SetAnimation(1, "left_click", false);

                track.Complete += _ =>
                {
                    _animationState.SetAnimation(1, "left_idle", true);
                };
            }
            else if (keyCode == KeyCode.RightArrow ||  keyCode == KeyCode.D)
            {
                var track = _animationState.SetAnimation(0, "right_click", false);
                
                track.Complete += _ =>
                {
                    _animationState.SetAnimation(0, "right_idle", true);
                };
            }
        }

        private void EventListen(TrackEntry trackEntry, Spine.Event e)
        {
            
        }
        
        private void PlayAnimation()
        {
            
        }
    }
}