using Game.Scripts.PcManagers;
using Game.Scripts.Player;
using Game.Scripts.Tick;
using UnityEngine;

namespace Game.Scripts.Root
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private TickManager _tickManager;
        [SerializeField] private HandsManager _handsManager;
        [SerializeField] private PcInitializer _pcInitializer;
        
        private void Start()
        {
            RegisterAll();
        }

        private void RegisterAll()
        {
            G.Register(new BufferManager());
            G.Register(_inputManager);
            G.Register(_tickManager);
            G.Register(_handsManager);
            G.Register(_pcInitializer);
            G.Register(new GameBootstrap());
            
            G.InitializeAll();
        }
    }
}