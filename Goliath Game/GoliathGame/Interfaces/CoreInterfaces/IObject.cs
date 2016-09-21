using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GoliathGame.Interfaces.AnimationInterfaces
{
    public interface IObject
    {
        void LoadContent(ContentManager theContentManager, string theAssetName);
        void Update(GameTime theGameTime);
        void Draw(SpriteBatch theSpriteBatch);
    }
}
