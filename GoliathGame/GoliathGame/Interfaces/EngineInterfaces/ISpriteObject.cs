using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GoliathGame.Interfaces.EngineInterfaces
{
    interface ISpriteObject
    {
        void LoadContent(ContentManager theContentManager, string theAssetName);
        void Update(GameTime theGameTime); //Need thinking on this
        void Draw(SpriteBatch theSpriteBatch);
    }
}
