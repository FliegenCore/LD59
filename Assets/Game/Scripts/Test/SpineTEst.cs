using Game.Scripts.Root;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Test
{
    public class SpineTEst : MonoBehaviour, IInitializable
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SpineBone] public string _boneName;

        private Bone _bone;

        private void Awake()
        {
            _bone = _skeletonAnimation.Skeleton.FindBone(_boneName);
        }
        
        private void Update()
        {
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            _bone.X = worldMousePosition.x;
            _bone.Y = worldMousePosition.y;
        }

        public void Initialize()
        {
            
        }
    }
}   
