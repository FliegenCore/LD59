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
        
        private void Start()
        {
            RegisterAll();
        }

        private void RegisterAll()
        {
            G.Register(_inputManager);
            G.Register(_tickManager);
            G.Register(_handsManager);
            G.Register(new GameBootstrap());
            
            G.InitializeAll();
        }
    }
}