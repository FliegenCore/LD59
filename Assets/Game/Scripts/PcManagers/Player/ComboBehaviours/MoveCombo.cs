using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;
using Game.Scripts.Tick;
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
            _playerView.PlayAnimation("walk", true);
            _isPlay = true;
            
            Observable.EveryUpdate()
                .Subscribe(_ => SelfUpdate())
                .AddTo(_disposables);
        }

        public override void Stop()
        {
            _playerView.PlayAnimation("idle", true);
            _disposables?.Clear();
            _isPlay = false;
        }

        public override bool CanPlay()
        {
            if (!G.Get<Raycaster>().TryGetPatient(out var f, _playerView.transform))
            {
                return true;
            }

            return false;
        }

        public override void Reset()
        {
            _nextMoveX = 0;
        }

        private void Move()
        {
            if (!_isPlay) return;
            Vector3 needPosition = new Vector3(_nextMoveX, _playerView.transform.position.y, 0);
            float step = 3 * Time.deltaTime;
            _playerView.transform.position = Vector3.MoveTowards(_playerView.transform.position, needPosition, step);
            Vector3 newPosition = _playerView.transform.position;
    
            float distance = Vector3.Distance(needPosition, _playerView.transform.position);
            
            if (distance < 0.01)
            {
                newPosition.x = _nextMoveX;
                _playerView.transform.position = newPosition;
                OnComplete?.Invoke();
            }
        }
        
        private void SelfUpdate()
        {
            Move();
        }
    }
}