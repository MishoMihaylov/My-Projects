using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Interfaces.Core.Animation;

namespace TowerDefence.Interfaces.Core
{
    public interface IGameObject : ILoad, IUnload, IUpdate, IDraw, IPositionable
    {
        Vector2 CenterPoint { get; }
    }
}
