using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Interfaces;
using GoliathGame.Models.UI;
using GoliathGame.Models.Utils;

namespace GoliathGame.Models.Units
{
    class Goliath : Unit
    {
        private const int AnimationWithSixAssetTotalFrames = 6;
        private const int StartingPositionX = 10;
        private const int StartingPositionY = 535;
        private const int GoliathSpeed = 230;
        private bool isAttackingNow;
        private double gravity = 5;
        private UIEventLog goliathEventLog;
        private UIHPbar goliathHpBar;
        private KeyboardState mPreviousKeyboardState;

        public Goliath()
        {
            this.goliathEventLog = new UIEventLog(this);
            this.goliathHpBar = new UIHPbar(this, new Vector2(2, 2), 20, 3, 1);
            this.AnimationAssetCurrentFrame = 1;
            this.reverseAnimation = false;
            this.currentState = State.IdleForward;
            this.AttackSpeedDelay = 0.3;
            this.AttackDamage = 20;
            this.IntervalBetweenAttack = TimeSpan.FromSeconds(AttackSpeedDelay + 0.1);
            this.isAttackingNow = false;         
        }

        public void LoadGoliathContent(ContentManager theContentManager)
        {
            this.Position = new Vector2(StartingPositionX, StartingPositionY);
            this.LoadContent(theContentManager, Constants.GoliathIdleForwardAssetPath); //TODO: To be removed
            this.UnitIdleForwardTexture = theContentManager.Load<Texture2D>(Constants.GoliathIdleForwardAssetPath);
            this.UnitIdleBackwardTexture = theContentManager.Load<Texture2D>(Constants.GoliathIdleBackwardAssetPath);
            this.UnitAttackForwardTexture = theContentManager.Load<Texture2D>(Constants.GoliathAttackForwardAssetPath);
            this.UnitAttackBackwardTexture = theContentManager.Load<Texture2D>(Constants.GoliathAttackBackwardAssetPath);
            this.UnitRunningForwardTexture = theContentManager.Load<Texture2D>(Constants.GoliathRunningForwardAssetPath);
            this.UnitRunningBackwardTexture = theContentManager.Load<Texture2D>(Constants.GoliathRunningBackwardAssetPath);
            this.UnitDeadTexture = theContentManager.Load<Texture2D>(Constants.GoliathDeadAssetPath);
            goliathEventLog.LoadContent(theContentManager, "Font");
            goliathHpBar.LoadContent(theContentManager, Constants.HealthBarPath);
        }

        public override void Update(GameTime theGameTime)
        {
            if (!this.IsDead())
            {
                UpdateState(theGameTime);

                this.UpdateGoliathState(theGameTime);
                this.goliathEventLog.Update(theGameTime);
                this.goliathHpBar.Update(theGameTime);
            }
            else
            {
                DeadAnimationUpdate(theGameTime);
            }

            this.Rectangle = new Rectangle((int)Position.X,
                (int)Position.Y, MainSpriteObjectTexture.Width, MainSpriteObjectTexture.Height);
        }

        private void UpdateGoliathState(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMotionState(aCurrentKeyboardState);

            mPreviousKeyboardState = aCurrentKeyboardState;

            UpdatePosition(theGameTime, Speed, Direction);
        }

        public override void UpdatePosition(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            this.Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }

        private void UpdateMotionState(KeyboardState aCurrentKeyboardState)
        {

            Speed = Vector2.Zero;
            Direction = Vector2.Zero;
            if (!isAttackingNow)
            {
                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
                {
                    if (this.Position.X >= 0)
                    {
                        Speed.X = GoliathSpeed;
                        Direction.X = Constants.MoveLeft;
                    }
                    SwitchState(State.RunningBackward);
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
                {

                    if (this.Position.X <= 1200)
                    {
                        Speed.X = GoliathSpeed;
                        Direction.X = Constants.MoveRight;
                    }
                    SwitchState(State.RunningForward);
                }
                else
                {
                    SwitchState(State.IdleForward);
                }
            }
                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true && this.Position.Y >= 530)
                {
                    Speed.Y = GoliathSpeed;
                    Direction.Y = Constants.MoveUp;
                    gravity = -5;
                }
                else
                {
                    if (this.Position.Y < 530)
                    {
                        Speed.Y = GoliathSpeed;
                        Direction.Y = (float)gravity;
                    }
                    if (gravity <= 5)
                    {
                        gravity += 0.15;
                    }
                }
            

            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true)
            {
                this.isAttackingNow = true;

                if (currentState == State.RunningForward || currentState == State.IdleForward)
                {
                    SwitchState(State.AttackForward);
                }
                else if (currentState == State.RunningBackward || currentState == State.IdleBackward)
                {
                    SwitchState(State.AttackBackward);
                }
                //else if (GoliathCurrentState == State.AttackForward || GoliathCurrentState == State.AttackBackward)
                //{
                //    if (canAttack)
                //    {
                //        if(GoliathCurrentState == State.AttackForward) 
                //        {
                //            SwitchState(State.AttackForward);
                //        }
                //        else 
                //        { 
                //            SwitchState(State.AttackBackward); 
                //        }
                //    }
                //}
            }
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (!this.IsDead())
            {
                DrawState(theSpriteBatch);
                this.goliathEventLog.Draw(theSpriteBatch);
                this.goliathHpBar.Draw(theSpriteBatch);
                //base.Draw(theSpriteBatch);
            }
            else
            {
                DeadAnimationDraw(theSpriteBatch);
            }
        }

        public override void AttackForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = (int)(AttackSpeedDelay * 1000) / (AnimationWithTenTotalFrames - 1);
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    this.AnimationAssetCurrentFrame--;
                    this.isAttackingNow = false;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public override void AttackBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = (int)(AttackSpeedDelay * 1000) / (AnimationWithTenTotalFrames - 1);
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    this.AnimationAssetCurrentFrame--;
                    this.isAttackingNow = false;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public override void IdleForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 100;

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.AnimationAssetCurrentFrame == AnimationWithSixAssetTotalFrames - 1)
                {
                    reverseAnimation = true;
                }
                else if (this.AnimationAssetCurrentFrame == 1)
                {
                    reverseAnimation = false;
                }

                if (!reverseAnimation)
                {
                    this.AnimationAssetCurrentFrame++;
                }
                else
                {
                    this.AnimationAssetCurrentFrame--;
                }
                this.timeSinceLastFrame = 0;
            }
        }

        public override void IdleBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 100;

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.AnimationAssetCurrentFrame == AnimationWithSixAssetTotalFrames - 1)
                {
                    reverseAnimation = true;
                }
                else if (this.AnimationAssetCurrentFrame == 1)
                {
                    reverseAnimation = false;
                }

                if (!reverseAnimation)
                {
                    this.AnimationAssetCurrentFrame++;
                }
                else
                {
                    this.AnimationAssetCurrentFrame--;
                }
                this.timeSinceLastFrame = 0;
            }
        }

        public void JumpAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }

        public override void DeadAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 150;

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.AnimationAssetCurrentFrame != AnimationWithTenTotalFrames - 1)
                {
                    this.AnimationAssetCurrentFrame++;
                }
            }
        }

        public override void AttackForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitAttackForwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitAttackForwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackForwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void AttackBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitAttackBackwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitAttackBackwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X - 85, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void IdleForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitIdleForwardTexture.Width;
            int height = UnitIdleForwardTexture.Height / AnimationWithSixAssetTotalFrames;
            int row = this.AnimationAssetCurrentFrame;

            Rectangle sourceImageRectangle = new Rectangle(0, height * this.AnimationAssetCurrentFrame, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitIdleForwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void IdleBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitIdleBackwardTexture.Width;
            int height = UnitIdleBackwardTexture.Height / AnimationWithSixAssetTotalFrames;
            int row = this.AnimationAssetCurrentFrame;

            Rectangle sourceImageRectangle = new Rectangle(0, height * this.AnimationAssetCurrentFrame, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitIdleBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }    

        public override void DeadAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitDeadTexture.Width / MartixAnimationAssetColumns;
            int height = UnitDeadTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitDeadTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void Strike(IUnit targetUnit, GameTime theGameTime)
        {
            //this.GoliathCurrentState = State.Attack;
            base.Strike(targetUnit, theGameTime);
        }
    }
}
