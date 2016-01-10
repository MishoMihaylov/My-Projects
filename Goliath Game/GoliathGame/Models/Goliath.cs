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
        IdleForward, IdleBackward, AttackForward, AttackBackward, RunningForward, RunningBackward
    }

    class Goliath : Unit
    {
        private const string GoliathMainAssetPath = "Goliath/IdleForward";
        private const string GoliathIdleBackwardAssetPath = "Goliath/IdleBackward";
        private const string GoliathAttackAssetPath = "Goliath/AttackForward";
        private const string GoliathAttackBackwardAssetPath = "Goliath/AttackBackward";
        private const string GoliathRunningForwardAssetPath = "Goliath/RunningForward";
        private const string GoliathRunningBackwardAssetPath = "Goliath/RunningBackward";
        private const string GoliathDeadAssetPath = "Goliath/Dead";
        private const int StartingPositionX = 10;
        private const int StartingPositionY = 500;
        private const int GoliathSpeed = 230;
        private const int MoveUp = -1;
        private const int MoveDown = 1;
        private const int MoveLeft = -1;
        private const int MoveRight = 1;
        private const int animationWithSixAssetTotalFrames = 6;
        private const int animationWithTenTotalFrames = 10;
        private const int MatrixAnimationAssetRows = 4;
        private const int MartixAnimationAssetColumns = 3;
        private int animationAssetCurrentFrame;
        private int timeSinceLastFrame;
        private bool reverseAnimation;
        private double GRAVITY = 5;
        private bool hasJumped = false; //need to be checked
        private UIEventLog GoliathEventLog;
        private UIHPbar GoliathHpBar;
        private KeyboardState mPreviousKeyboardState;
        State GoliathCurrentState;

        public Goliath()
        {
            this.GoliathEventLog = new UIEventLog(this);
            this.GoliathHpBar = new UIHPbar(this, new Vector2(2, 2), 20, 3, 1);
            this.animationAssetCurrentFrame = 1;
            this.reverseAnimation = false;
            this.GoliathCurrentState = State.IdleForward;
            this.IntervalBetweenAttack = TimeSpan.FromSeconds(2); //setting new attack speed
        }

        public void LoadGoliathContent(ContentManager theContentManager)
        {
            Position = new Vector2(StartingPositionX, StartingPositionY);
            this.LoadContent(theContentManager, GoliathMainAssetPath);
            this.UnitIdleBackwardTexture = theContentManager.Load<Texture2D>(GoliathIdleBackwardAssetPath);
            this.UnitAttackForwardTexture = theContentManager.Load<Texture2D>(GoliathAttackAssetPath);
            this.UnitAttackBackwardTexture = theContentManager.Load<Texture2D>(GoliathAttackBackwardAssetPath);
            this.UnitRunningForwardTexture = theContentManager.Load<Texture2D>(GoliathRunningForwardAssetPath);
            this.UnitRunningBackwardTexture = theContentManager.Load<Texture2D>(GoliathRunningBackwardAssetPath);
            this.UnitDeadTexture = theContentManager.Load<Texture2D>(GoliathDeadAssetPath);
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
                    case State.AttackForward:
                        AttackAnimationUpdate(theGameTime);
                        break;
                    case State.AttackBackward:
                        AttackBackwardAnimationUpdate(theGameTime);
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

            if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
            {
                if (this.Position.X >= 0)
                {
                    Speed.X = GoliathSpeed;
                    Direction.X = MoveLeft;
                }
                SwitchState(State.RunningBackward);
            }
            else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
            {

                if (this.Position.X <= 850)
                {
                    Speed.X = GoliathSpeed;
                    Direction.X = MoveRight;
                }
                SwitchState(State.RunningForward);
            }
            else
            {
                SwitchState(State.IdleForward);
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true && this.Position.Y >= 500)
            {
                Speed.Y = GoliathSpeed;
                Direction.Y = MoveUp;
                GRAVITY = -5;
            }
            else
            {
                if (this.Position.Y < 500)
                {
                    Speed.Y = GoliathSpeed;
                    Direction.Y = (float)GRAVITY;
                }
                if (GRAVITY <= 5)
                {
                    GRAVITY += 0.15;
                }
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true)
            {           
                    if (GoliathCurrentState == State.RunningForward || GoliathCurrentState == State.IdleForward)
                    {
                        SwitchState(State.AttackForward);
                    }
                    else if (GoliathCurrentState == State.RunningBackward || GoliathCurrentState == State.IdleBackward)
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
                switch (GoliathCurrentState)
                {
                    case State.IdleForward:
                        IdleForwardAnimationDraw(theSpriteBatch);
                        break;
                    case State.IdleBackward:
                        IdleBackwardAnimationDraw(theSpriteBatch);
                        break;
                    case State.AttackForward:
                        AttackAnimationDraw(theSpriteBatch);
                        break;
                    case State.AttackBackward:
                        AttackBackwardAnimationDraw(theSpriteBatch);
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
            else
            {
                DeadAnimationDraw(theSpriteBatch);
            }
        }

        public override void IdleForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 100;

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.animationAssetCurrentFrame == animationWithSixAssetTotalFrames - 1)
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
            int height = MainSpriteObjectTexture.Height / animationWithSixAssetTotalFrames;
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
                if (this.animationAssetCurrentFrame == animationWithSixAssetTotalFrames - 1)
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
            int height = UnitIdleBackwardTexture.Height / animationWithSixAssetTotalFrames;
            int row = this.animationAssetCurrentFrame;

            Rectangle sourceImageRectangle = new Rectangle(0, height * this.animationAssetCurrentFrame, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitIdleBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void AttackAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 10;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.animationAssetCurrentFrame++;
                if (this.animationAssetCurrentFrame == animationWithTenTotalFrames - 1)
                {
                    this.animationAssetCurrentFrame--;
                    SwitchState(State.IdleForward);
                }

                this.timeSinceLastFrame = 0;
            }
        }
        public override void AttackAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitAttackForwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitAttackForwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.animationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.animationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackForwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public void AttackBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 25;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.animationAssetCurrentFrame == animationWithTenTotalFrames - 1)
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
        public void AttackBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitAttackBackwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitAttackBackwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.animationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.animationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X - 85, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void JumpAnimationUpdate(GameTime theGameTime)
        {
            throw new NotImplementedException();
        }
        public override void JumpAnimationDraw(SpriteBatch theSpriteBatch) { }

        public override void DeadAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 150;
            
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.animationAssetCurrentFrame != animationWithTenTotalFrames - 1)
                {
                    this.animationAssetCurrentFrame++;
                }
            }
        }
        public override void DeadAnimationDraw(SpriteBatch theSpriteBatch) 
        {
            int width = UnitDeadTexture.Width / MartixAnimationAssetColumns;
            int height = UnitDeadTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.animationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.animationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitDeadTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public override void RunningForwardAnimationUpdate(GameTime theGameTime)
        {

            int milisecondsPerFrame = 40;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.animationAssetCurrentFrame == animationWithTenTotalFrames - 1)
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
                if (this.animationAssetCurrentFrame == animationWithTenTotalFrames - 1)
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
                    if(GoliathCurrentState == State.RunningBackward || GoliathCurrentState == State.RunningForward)
                    {
                        SwitchInProperIdleState();
                    }
                    else if (GoliathCurrentState == State.AttackBackward || GoliathCurrentState == State.AttackForward)
                    {
                        SwitchInProperIdleState();
                    }
                }
                else
                {
                    this.GoliathCurrentState = switchingState;
                    this.animationAssetCurrentFrame = 1;
                }
            }
        }

        private void SwitchInProperIdleState()
        {
            switch (this.GoliathCurrentState)
            {
                case State.AttackForward:
                    this.GoliathCurrentState = State.IdleForward;
                    this.animationAssetCurrentFrame = 1;
                    break;
                case State.AttackBackward:
                    this.GoliathCurrentState = State.IdleBackward;
                    this.animationAssetCurrentFrame = 1;
                    break;
                case State.RunningForward:
                    this.GoliathCurrentState = State.IdleForward;
                    this.animationAssetCurrentFrame = 1;
                    break;
                case State.RunningBackward:
                    this.GoliathCurrentState = State.IdleBackward;
                    this.animationAssetCurrentFrame = 1;
                    break;
            }
        }
       
    }
}
