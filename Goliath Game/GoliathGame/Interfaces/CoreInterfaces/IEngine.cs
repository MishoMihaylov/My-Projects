using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GoliathGame.Interfaces.AnimationInterfaces
{
    public interface IEngine : IObject
    {
        void Running(GameTime theGameTime);
    }
}
