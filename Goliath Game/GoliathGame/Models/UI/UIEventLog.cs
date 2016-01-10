using System;
using System.Collections.Generic;
using GoliathGame.Interfaces;
using GoliathGame.Interfaces.EngineInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GoliathGame.Models.UI
{
    enum KindOfBattleEvent
    {
        TakeDamage, Healing, IncreaseAttackDamage, IncreaseDefence
    }

    struct Log
    {
        private String logPoints;
        private KindOfBattleEvent eventType;
        private Color logColor;
        private Vector2 position;
        private TimeSpan eventOccuredTime;

        public Log(string logPoints, KindOfBattleEvent eventType, Vector2 position, TimeSpan EventOccuredTime)
        {
            this.logPoints = logPoints;
            this.eventType = eventType;
            this.logColor = new Color();
            this.position = position;
            this.eventOccuredTime = EventOccuredTime;

            switch (eventType)
            {
                case KindOfBattleEvent.Healing:
                    this.logColor = Color.LightGreen;
                    break;
                case KindOfBattleEvent.TakeDamage:
                    this.logColor = Color.Red;
                    break;
                case KindOfBattleEvent.IncreaseAttackDamage:
                    this.logColor = Color.MediumPurple;
                    break;
                case KindOfBattleEvent.IncreaseDefence:
                    this.logColor = Color.LightSkyBlue;
                    break;
                default: this.logColor = Color.White;
                    break;
            }
        }

        public string GetPoints
        {
            get { return this.logPoints; }
        }
        public Color GetColor
        {
            get { return this.logColor; }
        }
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public TimeSpan GetEventOccuredTime
        {
            get { return this.eventOccuredTime;}
        }
    }

    class UIEventLog : ISprite
    {
        private IUnit CurrentUnit;
        private List<Log> combatLog;
        private SpriteFont spriteFont;
        private int currentUnitDefence;
        private int currentUnitAttackDamage;
        private static readonly TimeSpan ShowTime = TimeSpan.FromSeconds(0.5);
        private int lastHealthUpdate;
        private int lastAttackDamageUpdate;
        private int lastDefenceUpdate;
        

        public UIEventLog(IUnit currentUnit)
        {
            this.currentUnitDefence = currentUnit.Health;
            this.currentUnitAttackDamage = currentUnit.Health;
            this.CurrentUnit = currentUnit;
            this.lastHealthUpdate = this.CurrentUnit.Health;
            this.lastAttackDamageUpdate = this.CurrentUnit.AttackDamage;
            this.lastHealthUpdate = this.CurrentUnit.Defence;
            combatLog = new List<Log>();
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            spriteFont = theContentManager.Load<SpriteFont>(theAssetName);
        }

        public void Update(GameTime theGameTime)
        {
            if (this.lastHealthUpdate != this.CurrentUnit.Health)
            {
                
                if (this.lastHealthUpdate < this.CurrentUnit.Health)
                {
                    combatLog.Add(new Log((Math.Abs(this.lastHealthUpdate - this.CurrentUnit.Health)).ToString(),
                    KindOfBattleEvent.Healing, new Vector2(this.Position.X + 85, this.Position.Y - 40), theGameTime.TotalGameTime));
                }
                else
                {
                    combatLog.Add(new Log((Math.Abs(this.lastHealthUpdate - this.CurrentUnit.Health)).ToString(),
                    KindOfBattleEvent.TakeDamage, new Vector2(this.Position.X + 85, this.Position.Y - 40), theGameTime.TotalGameTime));
                }
            }
            if (this.lastAttackDamageUpdate != this.CurrentUnit.AttackDamage)
            {
                combatLog.Add(new Log((Math.Abs(this.lastAttackDamageUpdate - this.CurrentUnit.AttackDamage)).ToString(),
                    KindOfBattleEvent.IncreaseAttackDamage, new Vector2(this.Position.X + 85, this.Position.Y - 40), theGameTime.TotalGameTime));
            }
            if (this.lastDefenceUpdate != this.CurrentUnit.Defence)
            {
                combatLog.Add(new Log((Math.Abs(this.lastDefenceUpdate - this.CurrentUnit.Defence)).ToString(),
                    KindOfBattleEvent.IncreaseDefence, new Vector2(this.Position.X + 85, this.Position.Y - 40), theGameTime.TotalGameTime));
            }
            
            this.lastHealthUpdate = this.CurrentUnit.Health;
            this.lastAttackDamageUpdate = this.CurrentUnit.AttackDamage;
            this.lastDefenceUpdate = this.CurrentUnit.Defence;

            for (int i = 0; i < combatLog.Count; i++)
            {
                if ((combatLog[i].GetEventOccuredTime + ShowTime) < theGameTime.TotalGameTime)
                {
                    combatLog.RemoveAt(i);
                }
            }

        }
        public void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (var log in combatLog)
            {
                if (log.GetColor == Color.Red)
                {
                    theSpriteBatch.DrawString(spriteFont, "-" + log.GetPoints, log.Position, log.GetColor);
                }
                else
                {
                    theSpriteBatch.DrawString(spriteFont, log.GetPoints, log.Position, log.GetColor);
                }
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.CurrentUnit.Position;
            }
            set
            {
                //throw new NotImplementedException();
            }
        }
    }
}

