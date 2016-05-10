using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using TowerDefence.Interfaces.InGame;

namespace TowerDefence.Models.InGame
{
    public class Level : ILevel
    {
        [XmlElement]
        public string BackgroundAssetPath { get; set; }
        [XmlElement]
        public Enemy[] Enemies { get; set; }
        [XmlElement]
        public List<Point> EnemiesPath { get; set; }
        [XmlElement]
        public Point EndPoint { get; set; }

        public Level()
        {

        }
    }
}
