using Game.Scripts.Player;
using Game.Scripts.Tick;
using UnityEngine;

namespace Game.Scripts.Root
{
    public class GameBootstrap : IInitializable
    {
        public void Initialize()
        {
            G.Get<TickManager>().Initialize();
            G.Get<InputManager>().Initialize();
            G.Get<HandsManager>().Initialize();
        }
    }
}