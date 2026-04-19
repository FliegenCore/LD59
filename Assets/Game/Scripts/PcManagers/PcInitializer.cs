using Game.Scripts.PcManagers.Player;
using Game.Scripts.Root;

namespace Game.Scripts.PcManagers
{
    public class PcInitializer
    {
        public void Initialize()
        {
            G.Get<PlayerManager>().Initialize();
        }
    }
}