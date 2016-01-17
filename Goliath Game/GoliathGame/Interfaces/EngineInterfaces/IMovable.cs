using Microsoft.Xna.Framework;

namespace GoliathGame.Interfaces.EngineInterfaces
{
    public interface IMovable
    {
        void UpdatePosition(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection);
    }
}
