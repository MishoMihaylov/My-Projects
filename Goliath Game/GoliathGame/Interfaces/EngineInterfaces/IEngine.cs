using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GoliathGame.Interfaces.EngineInterfaces
{
    interface IEngine : IObject
    {
        void Running(GameTime theGameTime);
        //void LoadContent(ContentManager theContentManager, string theAssetName);
        //void Update(GameTime theGameTime);
        // void Draw(SpriteBatch theSpriteBatch);
    }
}
