using System;
using System.Collections.Generic;
using Game.Scripts.PcManagers.Player.Impl;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private float _moveDistance;
        
        [SerializeField] private List<ComboConfig> _allCombos;
        [SerializeField] private List<ComboConfig> _startOpenedCombos;
        private PlayerView _playerView;
        private List<Combo> _combosBeh = new List<Combo>();
        private Dictionary<string, ComboConfig> _openedCombos = new ();
        private Combo _currentCombo;

        private BufferManager _bufferManager;
        
        
        public void Initialize()
        {
            _bufferManager = G.Get<BufferManager>();
            _playerView = G.Get<PlayerView>();
            _playerView.transform.localPosition = new Vector2(0, -0.49f);
            _combosBeh.Add(new Combo(GetComboConfigByUid("MoveCombo"), new MoveCombo(_playerView)));
            _combosBeh.Add(new Combo(GetComboConfigByUid("PillCombo"), new PillCombo(_playerView)));
            _combosBeh.Add(new Combo(GetComboConfigByUid("InjectionCombo"), new InjectionCombo(_playerView)));

            foreach (var start in _startOpenedCombos)
            {
                AddCombo(start);
            }
            
            _bufferManager.OnBufferFull += CheckCombo;
        }

        public void Reset()
        {
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

        public void AddCombo(ComboConfig combo)
        {
            Debug.Log("add combo " + combo.Uid);
            _openedCombos.Add(combo.Uid, combo);
            //клеим стикер
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
    }
}