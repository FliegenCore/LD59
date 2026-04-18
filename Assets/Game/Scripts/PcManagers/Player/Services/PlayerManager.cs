using System;
using System.Collections.Generic;
using Game.Scripts.PcManagers.Player.Impl;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Root;
using UnityEngine;

namespace Game.Scripts.PcManagers.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private float _moveDistance;
        
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private List<ComboConfig> _allCombos;
        private List<Combo> _combosBeh = new List<Combo>();
        private Combo _currentCombo;

        private BufferManager _bufferManager;

        
        //ОТКЛЮЧАТЬ ДВИЖЕНИЕ ПРИ ИЗМЕНЕНИИ БУФЕРА
        //ОТКЛЮЧАТЬ ДВИЖЕНИЕ ПРИ ИЗМЕНЕНИИ БУФЕРА
        //ОТКЛЮЧАТЬ ДВИЖЕНИЕ ПРИ ИЗМЕНЕНИИ БУФЕРА
        public void Initialize()
        {
            _bufferManager = G.Get<BufferManager>();
            _playerView.transform.localPosition = new Vector2(-2.85f, 0.41f);
            _combosBeh.Add(new Combo(GetComboConfigByUid("MoveCombo"), new MoveCombo(_playerView)));

            _bufferManager.OnBufferFull += CheckCombo;
        }

        private bool CheckCombo()
        {
            foreach (var combo in _combosBeh)
            {
                if (_bufferManager.HasCombo(combo.ComboKeys))
                {
                    _currentCombo = combo;
                    combo.Play();
                    return true;
                }
            }

            return false; 
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