using Game.Scripts.PcManagers.Player.View;
using UniRx;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player.Impl
{
    public class MoveCombo : AComboBehaviour
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        private bool _isPlay;
        private float _nextMoveX;
        private PlayerView _playerView;

        public MoveCombo(PlayerView playerView)
        {
            _playerView = playerView;
            _nextMoveX = playerView.transform.position.x;
        }
        
        public override void Play()
        {
            if (_isPlay)
                return;
                    
            _nextMoveX += 2f;
            _isPlay = true;
            
            Observable.EveryUpdate()
                .Subscribe(_ => SelfUpdate())
                .AddTo(_disposables);
        }

        public override void Stop()
        {
            _disposables?.Clear();
            _isPlay = false;
        }

        private void Move()
        {
            if (!_isPlay) return;

            Vector3 newPosition = _playerView.transform.position + Vector3.right * (3 * Time.deltaTime);
    
            if (newPosition.x >= _nextMoveX)
            {
                newPosition.x = _nextMoveX;
                _playerView.transform.position = newPosition;
                OnComplete?.Invoke();
            }
            else
            {
                _playerView.transform.position = newPosition;
            }
        }
        
        private void SelfUpdate()
        {
            Move();
        }
    }
}