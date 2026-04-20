using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.PcManagers.Level;
using Game.Scripts.PcManagers.Player.Impl;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;
using GameAnalyticsSDK;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private event Action _onCompleteCurrentAction;
        
        private float _moveDistance;

        public event Action<string> OnComboAdd;

        [SerializeField] private AudioSource _onDie;
        [SerializeField] private List<ComboConfig> _allCombos;
        [SerializeField] private List<ComboConfig> _startOpenedCombos;
        private PlayerView _playerView;
        private List<Combo> _combosBeh = new List<Combo>();
        private Dictionary<string, ComboConfig> _openedCombos = new ();
        private Combo _currentCombo;

        private bool _isDie;
        private BufferManager _bufferManager;
        
        public void Initialize()
        {
            _bufferManager = G.Get<BufferManager>();
            _playerView = G.Get<PlayerView>();
            _playerView.transform.localPosition = new Vector2(0, -0.49f);
            _combosBeh.Add(new Combo(GetComboConfigByUid("MoveCombo"), new MoveCombo(_playerView)));
            _combosBeh.Add(new Combo(GetComboConfigByUid("PillCombo"), new PillCombo(_playerView)));
            _combosBeh.Add(new Combo(GetComboConfigByUid("InjectionCombo"), new InjectionCombo(_playerView)));
            _combosBeh.Add(new Combo(GetComboConfigByUid("PistolCombo"), new PistolCombo(_playerView)));

            foreach (var start in _startOpenedCombos)
            {
                AddCombo(start);
            }
            
            _bufferManager.OnBufferFull += CheckCombo;
        }

        public void Reset()
        {
            _isDie = false;
            _playerView.transform.localPosition = new Vector2(0, -0.49f);
            _playerView.PlayAnimation("idle", true);

            foreach (var combo in _combosBeh)
            {
                combo.Reset();
            }
        }

        private bool CheckCombo()
        {
            foreach (var combo in _combosBeh)
            {
                if (!_openedCombos.ContainsKey(combo.Uid))
                {
                    continue;
                }
                
                if (_bufferManager.HasCombo(combo.ComboKeys))
                {
                    if (combo.CanPlay())
                    {
                        _currentCombo = combo;

                        if (_currentCombo != null)
                            _currentCombo.ComboBehaviour.OnComplete -= InvokeOnCompleteCurrentAction;
                        
                        _currentCombo.ComboBehaviour.OnComplete += InvokeOnCompleteCurrentAction;
                        combo.Play();
                        return true;
                    }

                    _playerView.PlayAnimation("mistake", false);
                    return false;
                }
            }
            _playerView.PlayAnimation("mistake", false);
            return false; 
        }

        public void SubscribeOnCompleteCurrentAction(Action action)
        {
            _onCompleteCurrentAction += action;
        }

        public void UnsubscribeOnCompleteCurrentAction(Action action)
        {
            _onCompleteCurrentAction -= action;
        }

        private void InvokeOnCompleteCurrentAction()
        {
            _onCompleteCurrentAction?.Invoke();
        }

        public void AddCombo(ComboConfig combo)
        {
            Debug.Log("add combo " + combo.Uid);
            if (_openedCombos.ContainsKey(combo.Uid))
                return;
            _openedCombos.Add(combo.Uid, combo);
            OnComboAdd?.Invoke(combo.Uid);
        }
        
        private ComboConfig GetComboConfigByUid(string uid)
        {
            foreach (var config in _allCombos)
            {
                if (config.Uid == uid)
                    return config;
            }

            return null;
        }

        public void PlayerDie()
        {
            if (_isDie)
                return;

            _isDie = true;
            StartCoroutine(PlayDieSound());
            _playerView.PlayAnimation("die", false, G.Get<LevelManager>().RestartCurrentLevel);
        }

        private IEnumerator PlayDieSound()
        {
            yield return new WaitForSeconds(0.5f);
            _onDie.Play();
        }
    }
}