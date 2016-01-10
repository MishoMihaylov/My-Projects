using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoliathGame.Models.UI;
using GoliathGame.Interfaces;
using GoliathGame.Interfaces.EngineInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GoliathGame.Models.Engine
{
    class Engine : IEngine
    {
        private HeroUI heroUI;
        private LinkedList<Unit> currentEnemies;
        private Potion potion;
        private bool potionOnTheField;
        private SharpingStone sharpingStone;
        private bool sharpingStoneOnTheField;
        private Armor armor;
        private bool armorOnTheField;
        private Goliath mainHero;
        private Enemy al, al1;
        private SpriteFont spriteFont;
        private KeyboardState currentKeyboardState;

        public Engine()
        {
            mainHero = new Goliath();
            al = new Enemy("Goliath/ToughStanding");
            al1 = new Enemy("Goliath/ToughStanding");
            this.currentEnemies = new LinkedList<Unit>();
            this.currentEnemies.AddLast(al);
            this.currentEnemies.AddLast(al1);
            heroUI = new HeroUI();
            potion = new Potion();
            potionOnTheField = false;
            sharpingStone = new SharpingStone();
            armor = new Armor();
            armorOnTheField = false;
            sharpingStoneOnTheField = false;
        }

        public void Running(GameTime theGameTime)
        {
            EnemiesMove(theGameTime); 

            currentKeyboardState = Keyboard.GetState();
            //Main Hero move
            if (currentKeyboardState.IsKeyDown(Keys.Space))
            {
                foreach (var enemy in currentEnemies)
                {
                    if (IsInRange(mainHero, enemy))
                    {
                        mainHero.Strike(enemy, theGameTime);
                    }
                }
            }
            CheckIfOverItem(mainHero);
            //Слагаме го заради движенията на главния герой
            Update(theGameTime);
        }

        public void Update(GameTime theGameTime)
        {
            mainHero.Update(theGameTime);
            foreach (var enemy in currentEnemies)
            {
                enemy.Update(theGameTime);
            }
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (var enemy in currentEnemies)
            {
                enemy.Draw(theSpriteBatch);
            }

            if (potionOnTheField)
            {
                potion.Draw(theSpriteBatch);
            }
            else if (sharpingStoneOnTheField)
            {
                sharpingStone.Draw(theSpriteBatch);
            }
            else if (armorOnTheField)
            {
                armor.Draw(theSpriteBatch);
            }

            mainHero.Draw(theSpriteBatch);
            theSpriteBatch.DrawString(spriteFont
                , String.Format("HP: {0} / 100 | AttackDamage: {1} | Defence: {2}",mainHero.Health.ToString(), mainHero.AttackDamage, mainHero.Defence),
                new Vector2(320, 2),
                Color.Yellow);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mainHero.LoadGoliathContent(theContentManager);
            al.LoadEnemyContent(theContentManager);
            al1.LoadEnemyContent(theContentManager);
            potion.LoadPotionContent(theContentManager);
            sharpingStone.LoadPotionContent(theContentManager);
            armor.LoadArmorContent(theContentManager);
            al1.Position = new Vector2(50, 500);
            spriteFont = theContentManager.Load<SpriteFont>("Font");
        }

        private bool IsInRange(IUnit attackingUnit, IUnit attackedUnit)
        {
            if (attackedUnit.AttackingRange <= Math.Abs(GetDistanceX(attackingUnit, attackedUnit)))
            {
                return false;
            }
            return true;
        }
        
        private void EnemyMoveToTarget(Unit unit, Unit target, GameTime theGameTime)
        {
            Vector2 Direction = Vector2.Zero;
            const int MOVE_LEFT = -1;
            const int MOVE_RIGHT = 1;

            unit.Speed.X = 50;

            if (GetDistanceX(unit, target) < 0)
            {
                Direction.X = MOVE_RIGHT;
                unit.UpdatePosition(theGameTime, unit.Speed, Direction);
            }
            else
            {
                Direction.X = MOVE_LEFT;
                unit.UpdatePosition(theGameTime, unit.Speed, Direction);
            }
            
        }
        private int GetDistanceX(IUnit unitOne, IUnit unitTwo)
        {
            int exactDistance = (int)(unitOne.Position.X - unitTwo.Position.X);

            return exactDistance;
        }
        private void Strike(IUnit unit, IUnit target)
        {
            target.Health -= (unit.AttackDamage - target.Defence);
        }
        private void EnemiesMove(GameTime theGameTime)
        {
            CheckAndRemoveDeadEnemies();
            if (mainHero.IsDead())
            {

            }
            else
            {
                foreach (var enemy in currentEnemies)
                {
                    if (!IsInRange(enemy, mainHero))
                    {
                        EnemyMoveToTarget(enemy, mainHero, theGameTime);
                    }
                    else
                    {
                        enemy.Strike(mainHero, theGameTime);

                    }
                }
            }
        }
        private void CheckAndRemoveDeadEnemies()
        {
            var currentNode = currentEnemies.First;

            while (currentNode != null)
            {
                if (currentNode.Value.IsDead())
                {
                    var currentNoteToBeRemoved = currentNode;
                    EnemyDropItem(currentNode.Value);
                    currentNode = currentNode.Next;            
                    currentEnemies.Remove(currentNoteToBeRemoved);
                }
                else
                {
                    currentNode = currentNode.Next;
                }
            }
        }
        private void EnemyDropItem(IUnit dyingEnemy)
        {
            //must drop one of 3 items
            //if (!potionOnTheField)
            //{
            //    potion.Position = new Vector2(dyingEnemy.Position.X, dyingEnemy.Position.Y + 130);
            //    potionOnTheField = true;
            //}
            //if (!sharpingStoneOnTheField)
            //{
            //    sharpingStone.Position = new Vector2(dyingEnemy.Position.X, dyingEnemy.Position.Y + 130);
            //    sharpingStoneOnTheField = true;
            //}
            if (!armorOnTheField)
            {
                armor.Position = new Vector2(dyingEnemy.Position.X, dyingEnemy.Position.Y + 130);
                armorOnTheField = true;
            }
        }
        private void CheckIfOverItem(IUnit Unit)
        {
            //if (Math.Abs(Unit.Position.X - potion.Position.X) < 50 && potionOnTheField)
            //{
            //    potion.Heal(Unit);
            //    potionOnTheField = false;
            //}
            //if (Math.Abs(Unit.Position.X - sharpingStone.Position.X) < 50 && sharpingStoneOnTheField)
            //{
            //    sharpingStone.SharpWeapon(Unit);
            //    sharpingStoneOnTheField = false;
            //}
            if (Math.Abs(Unit.Position.X - armor.Position.X) < 50 && armorOnTheField)
            {
                armor.Equip(Unit);
                armorOnTheField = false;
            }
        }

    }
}
