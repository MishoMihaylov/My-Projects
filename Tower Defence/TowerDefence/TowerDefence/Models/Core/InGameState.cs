using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Interfaces.InGame;
using TowerDefence.Interfaces.Core;
using TowerDefence.Models.InGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence.Models.Core
{
    public class InGameState : IInGameState
    {
        public InGameState(int runLevel, IXmlManager levelLoader)
        {
            this.CurrentLevel = runLevel;
            this.Levels = levelLoader.Load("asd");
        }

        public InGameState(int runLevel) : this(runLevel, new XmlManager<ILevel>(Level))
        {
            
        }

        private ILevel[] Levels { get; set; }

        private int CurrentLevel { get; set; }

        private int CurrentWave { get; set; }

        public void Run()
        {

        }

        public void LoadContent(ContentManager theContentManager)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        
    }
}
