using System;
using GoliathGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoliathGame.Models.Units
{
    public enum State
    {
        IdleForward, IdleBackward, AttackForward, AttackBackward, RunningForward, RunningBackward
    }

    public abstract class Unit : SpriteObject, IUnit, IAnimation
    {
        public Vector2 Direction = Vector2.Zero;
        public Vector2 Speed = Vector2.Zero;
        public State currentState;
        protected const int AnimationWithTenTotalFrames = 10;
        protected const int MatrixAnimationAssetRows = 4;
        protected const int MartixAnimationAssetColumns = 3;        
        protected bool canAttack = true;
        protected int timeSinceLastFrame;
        protected bool reverseAnimation;
        protected TimeSpan intervalBetweenAttack = TimeSpan.FromSeconds(1);
        private int health = 100;
        private int attackDamage = 7;
        private int attackingRange = 150;
        private double attackSpeedDelay = 1;
        private int defence = 5;
        private int animationAssetCurrentFrame;
        private TimeSpan lastTimeAttack;

        public Texture2D UnitIdleForwardTexture { get; set; }

        public Texture2D UnitIdleBackwardTexture { get; set; }

        public Texture2D UnitAttackForwardTexture { get; set; }

        public Texture2D UnitAttackBackwardTexture { get; set; }

        public Texture2D UnitRunningForwardTexture { get; set; }

        public Texture2D UnitRunningBackwardTexture { get; set; }

        public Texture2D UnitDeadTexture { get; set; }

        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                if (value <= 0)
                {
                    //throw new ArgumentOutOfRangeException("Unit's health cannot be <= 0");
                }
                this.health = value;
            }
        }

        public int AttackDamage
        {
            get
            {
                return this.attackDamage;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Unit's attackDamage cannot be <= 0");
                }
                this.attackDamage = value;
            }
        }

        public int AttackingRange
        {
            get
            {
                return attackingRange;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Unit's defence cannot be <= 0");
                }
                this.attackingRange = value;
            }
        }

        public double AttackSpeedDelay
        {
            get { return this.attackSpeedDelay; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("AttackSpeed must be more >= 0!");
                }
                this.attackSpeedDelay = value;
            }
        }

        public int Defence
        {
            get
            {
                return this.defence;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Unit's defence cannot be <= 0");
                }
                this.defence = value;
            }
        }

        public TimeSpan IntervalBetweenAttack 
        {
            get { return this.intervalBetweenAttack; }
            set { this.intervalBetweenAttack = value; }
        }

        public Rectangle Rectangle { get; set; }

        public abstract void UpdatePosition(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection);

        public virtual void Strike(IUnit targetUnit, GameTime theGameTime)
        {
            if(lastTimeAttack + intervalBetweenAttack < theGameTime.TotalGameTime)
            {
                canAttack = true;
            }
            else
            { 
                canAttack = false;
            }

            if (canAttack) 
            {
                targetUnit.Health -= this.AttackDamage - targetUnit.Defence;
                lastTimeAttack = theGameTime.TotalGameTime;
            }
        }

        public bool IsDead()
        {
            if (this.Health <= 0)
            {
                return true;
            }

            return false;
        }

        public virtual void AttackForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = (int)(AttackSpeedDelay * 1000) / (AnimationWithTenTotalFrames - 1);

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public virtual void AttackBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = (int)(AttackSpeedDelay * 1000) / (AnimationWithTenTotalFrames - 1);

            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public virtual void RunningForwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 50;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public virtual void RunningBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 50;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                this.AnimationAssetCurrentFrame++;
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    this.AnimationAssetCurrentFrame = 0;
                }

                this.timeSinceLastFrame = 0;
            }
        }

        public virtual void IdleForwardAnimationUpdate(GameTime theGameTime)
        {

            int milisecondsPerFrame = 90;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    reverseAnimation = true;
                }
                else if (this.AnimationAssetCurrentFrame == 0)
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

        public virtual void IdleBackwardAnimationUpdate(GameTime theGameTime)
        {
            int milisecondsPerFrame = 90;
            this.timeSinceLastFrame += theGameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milisecondsPerFrame)
            {
                if (this.AnimationAssetCurrentFrame == AnimationWithTenTotalFrames - 1)
                {
                    reverseAnimation = true;
                }
                else if (this.AnimationAssetCurrentFrame == 0)
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

        public virtual void DeadAnimationUpdate(GameTime theGameTime)
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

        public virtual void AttackForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitAttackForwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitAttackForwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackForwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public virtual void AttackBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitAttackBackwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitAttackBackwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((float)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X - 85, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public virtual void RunningForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitRunningForwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitRunningForwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitRunningForwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public virtual void RunningBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitRunningBackwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitRunningBackwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitRunningBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public virtual void IdleForwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitIdleForwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitIdleForwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitIdleForwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);

        }

        public virtual void IdleBackwardAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitAttackBackwardTexture.Width / MartixAnimationAssetColumns;
            int height = UnitAttackBackwardTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitAttackBackwardTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }

        public virtual void DeadAnimationDraw(SpriteBatch theSpriteBatch)
        {
            int width = UnitDeadTexture.Width / MartixAnimationAssetColumns;
            int height = UnitDeadTexture.Height / MatrixAnimationAssetRows;
            int row = (int)((double)this.AnimationAssetCurrentFrame / MartixAnimationAssetColumns);
            int column = this.AnimationAssetCurrentFrame % MartixAnimationAssetColumns;

            Rectangle sourceImageRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationImageRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);

            theSpriteBatch.Draw(this.UnitDeadTexture, destinationImageRectangle, sourceImageRectangle, Color.White);
        }              
        
        public virtual void SwitchState(State switchingState)
        {
            if (currentState != switchingState)
            {
                if (switchingState == State.IdleBackward || switchingState == State.IdleForward)
                {
                    if (currentState == State.RunningBackward || currentState == State.RunningForward)
                    {
                        SwitchInProperIdleState();
                    }
                    else if (currentState == State.AttackBackward || currentState == State.AttackForward)
                    {
                        SwitchInProperIdleState();
                    }
                }
                else if (switchingState == State.AttackBackward || switchingState == State.AttackForward)
                {
                    if (currentState == State.RunningBackward || currentState == State.RunningForward)
                    {
                        SwitchInProperAttackState();
                    }
                    else if (currentState == State.IdleBackward || currentState == State.IdleForward)
                    {
                        SwitchInProperAttackState();
                    }
                }
                else
                {
                    this.currentState = switchingState;
                    this.AnimationAssetCurrentFrame = 1;
                }
            }
        }

        public void SwitchInProperIdleState()
        {
            switch (this.currentState)
            {
                case State.AttackForward:
                    this.currentState = State.IdleForward;
                    this.AnimationAssetCurrentFrame = 1;
                    break;
                case State.AttackBackward:
                    this.currentState = State.IdleBackward;
                    this.AnimationAssetCurrentFrame = 1;
                    break;
                case State.RunningForward:
                    this.currentState = State.IdleForward;
                    this.AnimationAssetCurrentFrame = 1;
                    break;
                case State.RunningBackward:
                    this.currentState = State.IdleBackward;
                    this.AnimationAssetCurrentFrame = 1;
                    break;
            }
        }

        public void SwitchInProperAttackState()
        {
            switch (this.currentState)
            {
                case State.IdleForward:
                    this.currentState = State.AttackForward;
                    this.AnimationAssetCurrentFrame = 1;
                    break;
                case State.IdleBackward:
                    this.currentState = State.AttackBackward;
                    this.AnimationAssetCurrentFrame = 1;
                    break;
                case State.RunningForward:
                    this.currentState = State.AttackForward;
                    this.AnimationAssetCurrentFrame = 1;
                    break;
                case State.RunningBackward:
                    this.currentState = State.AttackBackward;
                    this.AnimationAssetCurrentFrame = 1;
                    break;
            }
        }

        protected void UpdateState(GameTime theGameTime)
        {
            switch (currentState)
            {
                case State.IdleForward:
                    IdleForwardAnimationUpdate(theGameTime);
                    break;
                case State.IdleBackward:
                    IdleBackwardAnimationUpdate(theGameTime);
                    break;
                case State.AttackForward:
                    AttackForwardAnimationUpdate(theGameTime);
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
        }

        protected void DrawState(SpriteBatch theSpriteBatch)
        {
            switch (currentState)
            {
                case State.IdleForward:
                    IdleForwardAnimationDraw(theSpriteBatch);
                    break;
                case State.IdleBackward:
                    IdleBackwardAnimationDraw(theSpriteBatch);
                    break;
                case State.AttackForward:
                    AttackForwardAnimationDraw(theSpriteBatch);
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
        }

        protected int AnimationAssetCurrentFrame
        {
            get { return this.animationAssetCurrentFrame; }
            set
            {
                if (value < 0)
                {
                    throw new IndexOutOfRangeException("Animation current frame cannot be < 0!");
                }

                this.animationAssetCurrentFrame = value;
            }
        }        
    }
}
