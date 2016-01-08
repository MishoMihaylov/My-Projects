using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Interfaces;
using GoliathGame.Models.UI;
using GoliathGame;

namespace GoliathGame.Models
{
    public enum State
    {
        IdleForward, IdleBackward, Attack, RunningForward, RunningBackward
    }

    class Goliath : Unit
    {
        private const string GoliathMainAssetPath = "Goliath/IdleForward";
        private const string GoliathIdleBackwardAssetPath = "Goliath/IdleBackward";
        private const string GoliathAttackAssetPath = "Goliath/Attack";
        private const string GoliathRunningForwardAssetPath = "Goliath/RunningForward";
        private const string GoliathRunningBackwardAssetPath = "Goliath/RunningBackward";
        private const int START_POSITION_X = 10;
        private const int START_POSITION_Y = 500;
        private const int GOLIATH_SPEED = 230;
        private const int MOVE_UP = -1;
        private const int MOVE_DOWN = 1;
        private const int MOVE_LEFT = -1;
        private const int MOVE_RIGHT = 1;
        private const int animationIdleAssetTotalFrames = 6;
        private const int animationAttachTotalFrames = 10;
        private const int MatrixAnimationAssetRows = 4;
        private const int MartixAnimationAssetColumns = 3;
        private int animationAssetCurrentFrame;
        private int timeSinceLastFrame;
        private bool reverseAnimation;
        //private const TimeSpan milisecondsPerFrame
        UIEventLog GoliathEventLog;
        UIHPbar GoliathHpBar;
        private double GRAVITY = 5;
        private bool hasJumped = false; //need to be checked
        State GoliathCurrentState;

        private KeyboardState mPreviousKeyboardState;

        public Goliath()
        {
            this.GoliathEventLog = new UIEventLog(this);
            this.GoliathHpBar = new UIHPbar(this, new Vector2(2, 2), 20, 3, 1);
            this.animationAssetCurrentFrame = 1;
            this.reverseAnimation = false;
            this.GoliathCurrentState = State.IdleForward;
            this.IntervalBetweenAttack = TimeSpan.FromSeconds(0.3);
        }

        public void LoadGoliathContent(ContentManager theContentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            this.LoadContent(theContentManager, GoliathMainAssetPath);
            this.UnitIdleBackwardTexture = theContentManager.Load<Texture2D>(GoliathIdleBackwardAssetPath);
            this.UnitAttackTexture = theContentManager.Load<Texture2D>(GoliathAttackAssetPath);
            this.UnitRunningForwardTexture = theContentManager.Load<Texture2D>(GoliathRunningForwardAssetPath);
            this.UnitRunningBackwardTexture = theContentManager.Load<Texture2D>(GoliathRunningBackwardAssetPath);
            GoliathEventLog.LoadContent(theContentManager, "Font");
            GoliathHpBar.LoadContent(theContentManager, "UIAttributes/HPBar/100");
        }

        public override void Update(GameTime theGameTime)
        {
            if (!this.IsDead())
            {
                switch (GoliathCurrentState)
                {
                    case State.IdleForward:
                        IdleForwardAnimationUpdate(theGameTime);
                        break;
                    case State.IdleBackward:
                        IdleBackwardAnimationUpdate(theGameTime);
                        break;
                    case State.Attack:
                        AttackAnimationUpdate(theGameTime);
                        break;
                    case State.RunningForward:
                        RunningForwardAnimationUpdate(theGameTime);
                        break;
                    case State.RunningBackward:
                        RunningBackwardAnimationUpdate(theGameTime);
                        break;
                }

                this.UpdateGoliathState(theGameTime);
                this.GoliathEventLog.Update(theGameTime);
                this.GoliathHpBar.Update(theGameTime);
                this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, MainSpriteObjectTexture.Width, MainSpriteObjectTexture.Height);
            }
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


            if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
            {
                if (this.Position.X >= 0)
                {
                    Speed.X = GOLIATH_SPEED;
                    Direction.X = MOVE_LEFT;
                }
                SwitchState(State.RunningBackward);
            }
            else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
            {

                if (this.Position.X <= 850)
                {
                    Speed.X = GOLIATH_SPEED;
                    Direction.X = MOVE_RIGHT;
                }
                SwitchState(State.RunningForward);
            }
            else
            {
                SwitchState(State.IdleForward);
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true && this.Position.Y >= 500)
            {
                Speed.Y = GOLIATH_SPEED;
                Direction.Y = MOVE_UP;
                GRAVITY = -5;
            }
            else
            {
                if (this.Position.Y < 500)
                {
                    Speed.Y = GOLIATH_SPEED;
                    Direction.Y = (float)GRAVITY;
                }
                if (GRAVITY <= 5)
                {
                    GRAVITY += 0.15;
                }
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true)
            {
                SwitchState(State.Attack);
            }

        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (!this.IsDead())
            {
                switch (GoliathCurrentState)
                {
                    case State.IdleForward:
                        IdleForwardAnimationDraw(theSpriteBatch);
                        break;
                    case State.IdleBackward:
                        IdleBackwardAnimationDraw(theSpriteBatch);
                        break;
                    case State.Attack:
                        AttackAnimationDraw(theSpriteBatch);
                        break;
                    case State.RunningForward:
                        RunningForwardAnimationDraw(theSpriteBatch);
                        break;
                    case State.RunningBackward:
                        RunningBackwardAnimationDraw(theSpriteBatch);
                        break;
                }
                this.GoliathEventLog.Draw(theSpriteBatch);
                this.GoliathHpBar.Draw(theSpriteBatch);
                //base.Draw(theSpriteBatch);
            }
        }

        public override void IdleForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 100;
            
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.animationAssetCurrentFrame == animationIdleAssetTotalFrames - 1)
                {
                    reverseAnimation = true;
                }
                else if (this.animationAssetCurrentFrame == 1)
                {
                    reverseAnimation = false;
                }

                if (!reverseAnimation)
                {
                    this.animationAssetCurrentFrame++;
                }
                else
                {
                    this.animationAssetCurrentFrame--;
                }
                this.timeSinceLastFrame = 0;
            }
        }       
        public override void IdleForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = MainSpriteObjectTexture.Width;
            int height = MainSpriteObjectTexture.Height / animationIdleAssetTotalFrames;
            int row = this.animationAssetCurrentFrame;

            Rectangle sourceImageRectangle = new Rectangle(0, height * this.animationAssetCurrentFrame, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.MainSpriteObjectTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void IdleBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 100;

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.animationAssetCurrentFrame == animationIdleAssetTotalFrames - 1)
                {
                    reverseAnimation = true;
                }
                else if (this.animationAssetCurrentFrame == 1)
                {
                    reverseAnimation = false;
                }

                if (!reverseAnimation)
                {
                    this.animationAssetCurrentFrame++;
                }
                else
                {
                    this.animationAssetCurrentFrame--;
                }
                this.timeSinceLastFrame = 0;
            }
        }
        public override void IdleBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitIdleBackwardTexture.Width;
            int height = UnitIdleBackwardTexture.Height / animationIdleAssetTotalFrames;
            int row = this.animationAssetCurrentFrame;

            Rectangle sourceImageRectangle = new Rectangle(0, height * this.animationAssetCurrentFrame, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitIdleBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void AttackAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 25;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.animationAssetCurrentFrame == animationAttachTotalFrames - 1)
                {
                    reverseAnimation = true;
                }
                else if (this.animationAssetCurrentFrame == 1)
                {
                    reverseAnimation = false;
                }

                if (!reverseAnimation)
                {
                    this.animationAssetCurrentFrame++;
                }
                else
                {
                    this.animationAssetCurrentFrame--;
                }

                this.timeSinceLastFrame = 0;
            }
        }
        public override void AttackAnimationDraw(SpriteBatch theSpriteBatch) 
        {
            int width = UnitAttackTexture.Width / MartixAnimationAssetColumns;
            int height = UnitAttackTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.animationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.animationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void JumpAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }
        public override void JumpAnimationDraw(SpriteBatch theSpriteBatch) { }

        public override void DeadAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }
        public override void DeadAnimationDraw(SpriteBatch theSpriteBatch) { }

        public override void RunningForwardAnimationUpdate(GameTime theGameTime)
        {

            int milisecondsPerFrame = 40;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.animationAssetCurrentFrame == animationAttachTotalFrames - 1)
                {
                    reverseAnimation = true;
                }
                else if (this.animationAssetCurrentFrame == 0)
                {
                    reverseAnimation = false;
                }

                if (!reverseAnimation)
                {
                    this.animationAssetCurrentFrame++;
                }
                else
                {
                    this.animationAssetCurrentFrame--;
                }

                this.timeSinceLastFrame = 0;
            }
        }
        public override void RunningForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitRunningForwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitRunningForwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.animationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.animationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitRunningForwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void RunningBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 40;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.animationAssetCurrentFrame == animationAttachTotalFrames - 1)
                {
                    reverseAnimation = true;
                }
                else if (this.animationAssetCurrentFrame == 1)
                {
                    reverseAnimation = false;
                }

                if (!reverseAnimation)
                {
                    this.animationAssetCurrentFrame++;
                }
                else
                {
                    this.animationAssetCurrentFrame--;
                }

                this.timeSinceLastFrame = 0;
            }
        }
        public override void RunningBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitRunningBackwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitRunningBackwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.animationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.animationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitRunningBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void Strike(IUnit targetUnit, GameTime theGameTime)
        {
            //this.GoliathCurrentState = State.Attack;
            base.Strike(targetUnit, theGameTime);
        }

        private void SwitchState(State switchingState)
        {
            if (GoliathCurrentState != switchingState)
            {
                if (switchingState == State.IdleBackward || switchingState == State.IdleForward)
                {
                    if (this.GoliathCurrentState == State.RunningForward)
                    {
                        this.GoliathCurrentState = State.IdleForward;
                        this.animationAssetCurrentFrame = 1;
                    }
                    else if(this.GoliathCurrentState == State.RunningBackward)
                    {
                        this.GoliathCurrentState = State.IdleBackward;
                        this.animationAssetCurrentFrame = 1;
                    }
                }
                else
                {
                    this.GoliathCurrentState = switchingState;
                    this.animationAssetCurrentFrame = 1;
                }          
            }
        }
        
    }
}
