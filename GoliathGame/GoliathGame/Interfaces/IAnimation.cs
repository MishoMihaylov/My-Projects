﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GoliathGame.Interfaces
{
    interface IAnimation
    {
        Texture2D UnitAttackTexture { get; set; }
        Texture2D UnitRunningForwardTexture { get; set; }
        void IdleForwardAnimationDraw(SpriteBatch theSpriteBatch);
        void IdleForwardAnimationUpdate(GameTime theGameTime);
        void IdleBackwardAnimationDraw(SpriteBatch theSpriteBatch);
        void IdleBackwardAnimationUpdate(GameTime theGameTime);
        void AttackAnimationDraw(SpriteBatch theSpriteBatch);
        void AttackAnimationUpdate(GameTime theGameTime);
        void JumpAnimationDraw(SpriteBatch theSpriteBatch);
        void JumpAnimationUpdate(GameTime theGameTime);
        void DeadAnimationDraw(SpriteBatch theSpriteBatch);
        void DeadAnimationUpdate(GameTime theGameTime);
        void RunningForwardAnimationUpdate(GameTime theGameTime);
        void RunningForwardAnimationDraw(SpriteBatch theSpriteBatch);
        void RunningBackwardAnimationUpdate(GameTime theGameTime);
        void RunningBackwardAnimationDraw(SpriteBatch theSpriteBatch);
    }
}
