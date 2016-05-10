using TowerDefence.Interfaces.Core.Animation;

namespace TowerDefence.Interfaces.Core
{
    public interface IInGameState : ILoad, IDraw, IUnload, IUpdate
    {
        void Run();
    }
}
