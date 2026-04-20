using Game.Scripts.PcManagers;
using Game.Scripts.PcManagers.Level;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.PcManagers.Player.Impl.Components;
using Game.Scripts.PcManagers.Player.View;
using Game.Scripts.Player;
using Game.Scripts.Tick;
using GameAnalyticsSDK;
using UnityEngine;

namespace Game.Scripts.Root
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private TickManager _tickManager;
        [SerializeField] private HandsManager _handsManager;
        [SerializeField] private BufferView _bufferView;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private StickManager _stickerManager;
        
        private void Start()
        {
            RegisterAll();
        }

        private void RegisterAll()
        {
            GameAnalytics.SetCustomId("myCustomUserId");
            GameAnalytics.Initialize();
            
            G.Register(new Raycaster());
            G.Register(_bufferView);
            G.Register(new BufferManager());
            G.Register(_inputManager);
            G.Register(_tickManager);
            G.Register(_handsManager);
            G.Register(_playerView);
            G.Register(_playerManager);
            G.Register(_levelManager);
            G.Register(_stickerManager);
            G.Register(new PcInitializer());
            G.Register(new GameBootstrap());
            
            G.InitializeAll();
            G.Get<PcInitializer>().Initialize();
        }
    }
}