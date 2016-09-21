using Microsoft.Xna.Framework;

namespace GoliathGame.Interfaces.AnimationInterfaces
{
    public interface IMovable
    {
        void UpdatePosition(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection);

        //Vector2 Direction { get; set; }

        //Vector2 Speed { get; set; }
    }
}
