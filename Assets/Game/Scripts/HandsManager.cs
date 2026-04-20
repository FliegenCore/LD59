using Game.Scripts.Player;
using Game.Scripts.Root;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

namespace Game.Scripts
{
    public class HandsManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _leftClickSource;
        [SerializeField] private AudioSource _rightClickSource;
        [SerializeField] private AudioSource _deadSound;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        
        private Spine.AnimationState _animationState;
        private InputManager _inputManager;

        public void Initialize()
        {
            _inputManager = G.Get<InputManager>();
            _animationState = _skeletonAnimation.AnimationState;
            _animationState.SetAnimation(0, "right_idle", true);
            _animationState.SetAnimation(1, "left_idle", true);
            _animationState.Event += EventListen;
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

        private void EventListen(TrackEntry trackEntry, Event e)
        {
            if (e.Data.Name == "left_click")
            {
                _leftClickSource.Play();
            }
            else if (e.Data.Name == "right_click")
            {
                _rightClickSource.Play();
            }
        }
        
        public void PlayEndAnimation()
        {
            _animationState.SetEmptyAnimation(0,0);
            _animationState.SetEmptyAnimation(1,0);
            _deadSound.Play();
            _animationState.SetAnimation(0, "handsOFF", false);
        }
    }
}