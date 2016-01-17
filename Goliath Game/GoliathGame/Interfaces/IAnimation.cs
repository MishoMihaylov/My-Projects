using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GoliathGame.Interfaces
{
    public interface IAnimation
    {
        Texture2D UnitIdleBackwardTexture {get; set;}
        Texture2D UnitAttackForwardTexture { get; set; }
        Texture2D UnitAttackBackwardTexture {get; set;}
        Texture2D UnitRunningForwardTexture {get; set;}
        Texture2D UnitRunningBackwardTexture {get; set;}
        Texture2D UnitDeadTexture { get; set; }
       
        //Update Methods
        void AttackForwardAnimationUpdate(GameTime theGameTime);
        void AttackBackwardAnimationUpdate(GameTime theGameTime);
        void RunningForwardAnimationUpdate(GameTime theGameTime);
        void RunningBackwardAnimationUpdate(GameTime theGameTime);
        void IdleForwardAnimationUpdate(GameTime theGameTime);
        void IdleBackwardAnimationUpdate(GameTime theGameTime);
        void JumpAnimationUpdate(GameTime theGameTime);
        void DeadAnimationUpdate(GameTime theGameTime);
        //Draw Methods
        void AttackForwardAnimationDraw(SpriteBatch theSpriteBatch);
        void AttackBackwardAnimationDraw(SpriteBatch theSpriteBatch);
        void RunningForwardAnimationDraw(SpriteBatch theSpriteBatch);
        void RunningBackwardAnimationDraw(SpriteBatch theSpriteBatch);
        void IdleForwardAnimationDraw(SpriteBatch theSpriteBatch);
        void IdleBackwardAnimationDraw(SpriteBatch theSpriteBatch);
        void JumpAnimationDraw(SpriteBatch theSpriteBatch);
        void DeadAnimationDraw(SpriteBatch theSpriteBatch);
    }
}
