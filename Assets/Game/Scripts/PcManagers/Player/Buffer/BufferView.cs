using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.PcManagers
{
    public class BufferView : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        
        private Spine.AnimationState _animationState;

        public void Initialize()
        {
            _animationState = _skeletonAnimation.AnimationState;
        }

        public void ShowValue(int index, KeyCode keyCode)
        {
            string code = keyCode == KeyCode.LeftArrow ? "0": "1";

            string animationName = $"slot{index}/{code}";
            _animationState.SetAnimation(index, animationName, false);
        }
        
        public void Clear()
        {
            var track = _animationState.SetAnimation(0, "body/mistake", false);
            
            track.Complete += _ =>
            {
                _animationState.SetAnimation(0, "body/idle", false);
                EmptyAll();
            };
        }

        public void ClearGoodAnimation()
        {
            var track = _animationState.SetAnimation(0, "body/done", false);

            track.Complete += _ =>
            {
                _animationState.SetAnimation(0, "body/idle", false);
                EmptyAll();
            };
        }

        private void EmptyAll()
        {
            _animationState.SetAnimation(1, "slot1/empty", true);
            _animationState.SetAnimation(2, "slot2/empty", true);
            _animationState.SetAnimation(3, "slot3/empty", true);
            _animationState.SetAnimation(4, "slot4/empty", true);
            
            _animationState.SetEmptyAnimation(1,0);
            _animationState.SetEmptyAnimation(2,0);
            _animationState.SetEmptyAnimation(3,0);
            _animationState.SetEmptyAnimation(4,0);
        }
    }
}