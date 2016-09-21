using System;
using System.Collections.Generic;
using GoliathGame.Models.UI;
using GoliathGame.Interfaces;
using GoliathGame.Interfaces.EngineInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GoliathGame.Models.Items;
using GoliathGame.Models.Units;

namespace GoliathGame.Models.Engine
{
    class Engine : IEngine
    {
        private HeroUI heroUI;
        private LinkedList<Enemy> currentEnemies;
        private SharpingStone sharpingStone;
        private Potion potion;
        private Armor armor;
        private Goliath mainHero;
        private Enemy al, al1;
        private SpriteFont spriteFont;
        private KeyboardState currentKeyboardState;
        private Background levelBackground;
        private bool sharpingStoneOnTheField;
        private bool potionOnTheField;
        private bool armorOnTheField;

        public Engine()
        {
            levelBackground = new Background();
            mainHero = new Goliath();
            al = new EnemyNinjaGirl();
            al1 = new EnemyGirl();
            this.currentEnemies = new LinkedList<Enemy>();
            this.currentEnemies.AddLast(al);
            this.currentEnemies.AddLast(al1);
            heroUI = new HeroUI();
            sharpingStone = new SharpingStone();
            potion = new Potion();
            armor = new Armor();
            sharpingStoneOnTheField = false;
            potionOnTheField = false;
            armorOnTheField = false;
        }

        public void Running(GameTime theGameTime)
        {
            EnemiesMove(theGameTime);
            HeroMove(theGameTime);     
            Update(theGameTime);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            levelBackground.LoadContent(theContentManager, "LevelsBackground/LevelOneBackground");
            mainHero.LoadGoliathContent(theContentManager);
            al.LoadEnemyContent(theContentManager);
            al1.LoadEnemyContent(theContentManager);
            potion.LoadPotionContent(theContentManager);
            sharpingStone.LoadPotionContent(theContentManager);
            armor.LoadArmorContent(theContentManager);
            spriteFont = theContentManager.Load<SpriteFont>("Font");
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
            levelBackground.Draw(theSpriteBatch);
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
                , String.Format("HP: {0} / 100 | AttackDamage: {1} | Defence: {2}", mainHero.Health.ToString(), mainHero.AttackDamage, mainHero.Defence),
                new Vector2(320, 2),
                Color.Yellow);
        }

        private bool IsInRange(IUnit attackingUnit, IUnit attackedUnit)
        {
            int distanceBetweenTheUnits = GetDistanceX(attackingUnit, attackedUnit);
            //TODO Attack range corrections
            if (distanceBetweenTheUnits > 0)
            {
                distanceBetweenTheUnits += 70;
            }

            if (attackedUnit.AttackingRange <= Math.Abs(distanceBetweenTheUnits))
            {
                return false;
            }
            return true;
        }

        private int GetDistanceX(IUnit unitOne, IUnit unitTwo)
        {
            int exactDistance = (int)(unitOne.Position.X - unitTwo.Position.X);

            return exactDistance;
        }

        private void EnemyMoveToTarget(Unit unit, Unit target, GameTime theGameTime)
        {
            Vector2 Direction = Vector2.Zero;
            const int MoveLeft = -1;
            const int MoveRight = 1;

            unit.Speed.X = 50;

            if (GetDistanceX(unit, target) < 0)
            {
                Direction.X = MoveRight;
                unit.UpdatePosition(theGameTime, unit.Speed, Direction);
            }
            else
            {
                Direction.X = MoveLeft;
                unit.UpdatePosition(theGameTime, unit.Speed, Direction);
            }

        }  

        private void Strike(IUnit unit, IUnit target)
        {
            target.Health -= (unit.AttackDamage - target.Defence);
        }

        private void EnemiesMove(GameTime theGameTime)
        {
            //CheckAndRemoveDeadEnemies();
            if (mainHero.IsDead())
            {
                //TODO
                foreach (var enemy in currentEnemies)
                {
                    enemy.SwitchState(State.IdleForward);
                }
            }
            else
            {
                foreach (var enemy in currentEnemies)
                {
                    if (!enemy.IsDead())
                    {
                        if (!IsInRange(enemy, mainHero))
                        {
                            if (enemy.currentState != State.RunningForward || enemy.currentState != State.RunningBackward)
                            {
                                if (GetDistanceX(mainHero, enemy) > 0)
                                {
                                    enemy.SwitchState(State.RunningForward);
                                }
                                else
                                {
                                    enemy.SwitchState(State.RunningBackward);
                                }

                            }

                            EnemyMoveToTarget(enemy, mainHero, theGameTime);
                        }
                        else
                        {
                            if (enemy.currentState != State.AttackForward) { enemy.SwitchState(State.AttackForward); };
                            enemy.Strike(mainHero, theGameTime);
                        }
                    }
                }
            }
        }

        private void CheckAndRemoveDeadEnemies()
        {
            var currentNode = currentEnemies.First;

            while (currentNode != null)
            {
                if (currentNode.Value.readyToBeRemoved)
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
            if (!potionOnTheField)
            {
                potion.Position = new Vector2(dyingEnemy.Position.X, dyingEnemy.Position.Y + 130);
                potionOnTheField = true;
            }
            if (!sharpingStoneOnTheField)
            {
                sharpingStone.Position = new Vector2(dyingEnemy.Position.X, dyingEnemy.Position.Y + 130);
                sharpingStoneOnTheField = true;
            }
            if (!armorOnTheField)
            {
                armor.Position = new Vector2(dyingEnemy.Position.X, dyingEnemy.Position.Y + 130);
                armorOnTheField = true;
            }
        }

        private void CheckIfOverItem(IUnit Unit)
        {
            if (Math.Abs(Unit.Position.X - potion.Position.X) < 50 && potionOnTheField)
            {
                potion.Heal(Unit);
                potionOnTheField = false;
            }
            if (Math.Abs(Unit.Position.X - sharpingStone.Position.X) < 50 && sharpingStoneOnTheField)
            {
                sharpingStone.SharpWeapon(Unit);
                sharpingStoneOnTheField = false;
            }
            if (Math.Abs(Unit.Position.X - armor.Position.X) < 50 && armorOnTheField)
            {
                armor.Equip(Unit);
                armorOnTheField = false;
            }
        }

        private void HeroMove(GameTime theGameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Space))
            {
                foreach (var enemy in currentEnemies)
                {
                    if (IsInRange(mainHero, enemy))
                    {
                        if (!enemy.IsDead())
                        {
                            mainHero.Strike(enemy, theGameTime);
                        }
                    }
                }
            }

            CheckIfOverItem(mainHero);
        }
    }
}
