namespace GoliathGame.Models
{
    using System;
    using Microsoft.Xna.Framework;

    class Background : SpriteObject
    {
        public Background()
        {
            this.Position = new Vector2(0, -30);

        }

        public override void Update(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }
    }
}