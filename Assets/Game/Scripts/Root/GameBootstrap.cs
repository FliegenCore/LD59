using Game.Scripts.PcManagers;
using Game.Scripts.PcManagers.Player;
using Game.Scripts.Player;
using Game.Scripts.Tick;

namespace Game.Scripts.Root
{
    public class GameBootstrap : IInitializable
    {
        public void Initialize()
        {
            G.Get<TickManager>().Initialize();
            G.Get<InputManager>().Initialize();
            G.Get<BufferManager>().Initialize();
            G.Get<HandsManager>().Initialize(); 
            G.Get<PlayerManager>().Initialize();
        }
    }
}