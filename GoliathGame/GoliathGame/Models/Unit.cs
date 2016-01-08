using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoliathGame.Interfaces;
using GoliathGame.Interfaces.EngineInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoliathGame.Models
{
    abstract class Unit : SpriteObject, IUnit, IAnimation
    {
        //Да се напише метод IsDead()
        private int health = 100;
        private int attackDamage = 7;
        private int attackingRange = 150;
        private int attackSpeedDelay = 1;
        private int defence = 5;
        public Vector2 Direction = Vector2.Zero;
        public Vector2 Speed = Vector2.Zero;
        private TimeSpan intervalBetweenAttack = TimeSpan.FromSeconds(1);
        private TimeSpan lastTimeAttack;
        private Texture2D unitIdleBackwardTexture;
        private Texture2D unitAttackTexture;
        private Texture2D unitRunningForwardTexture;
        private Texture2D unitRunningBackwardTexture;

        
        //AttackSpeed е направен за цел тест и има нужда от преправяне за да може да се променя.

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

        public int AttackSpeedDelay
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

        public Texture2D UnitIdleBackwardTexture
        {
            get
            {
                return this.unitIdleBackwardTexture;
            }
            set
            {
                this.unitIdleBackwardTexture = value;
            }
        }

        public Texture2D UnitAttackTexture
        {
            get
            {
                return this.unitAttackTexture;
            }
            set
            {
                this.unitAttackTexture = value;
            }
        }

        public Texture2D UnitRunningForwardTexture
        {
            get
            {
                return this.unitRunningForwardTexture;
            }
            set
            {
                this.unitRunningForwardTexture = value;
            }
        }

        public Texture2D UnitRunningBackwardTexture
        {
            get
            {
                return this.unitRunningBackwardTexture;
            }
            set
            {
                this.unitRunningBackwardTexture = value;
            }
        }

        public Rectangle Rectangle { get; set; }

        public abstract void UpdatePosition(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection);

        public virtual void Strike(IUnit targetUnit, GameTime theGameTime)
        {
            if (lastTimeAttack + intervalBetweenAttack < theGameTime.TotalGameTime) // need one =
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
        //TODO
        public void Die()
        {

        }

        public abstract void IdleForwardAnimationUpdate(GameTime theGameTime);
        public abstract void IdleForwardAnimationDraw(SpriteBatch theSpriteBatch);
        public abstract void IdleBackwardAnimationDraw(SpriteBatch theSpriteBatch);
        public abstract void IdleBackwardAnimationUpdate(GameTime theGameTime);
        public abstract void AttackAnimationUpdate(GameTime theGameTime);
        public abstract void AttackAnimationDraw(SpriteBatch theSpriteBatch);
        public abstract void JumpAnimationUpdate(GameTime theGameTime);
        public abstract void JumpAnimationDraw(SpriteBatch theSpriteBatch);
        public abstract void DeadAnimationUpdate(GameTime theGameTime);
        public abstract void DeadAnimationDraw(SpriteBatch theSpriteBatch);      
        public abstract void RunningForwardAnimationUpdate(GameTime theGameTime);
        public abstract void RunningForwardAnimationDraw(SpriteBatch theSpriteBatch);
        public abstract void RunningBackwardAnimationUpdate(GameTime theGameTime);
        public abstract void RunningBackwardAnimationDraw(SpriteBatch theSpriteBatch);
   
    }
}
