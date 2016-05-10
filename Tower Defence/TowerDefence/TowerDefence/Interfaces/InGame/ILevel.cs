using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Models.InGame;

namespace TowerDefence.Interfaces.InGame
{
    public interface ILevel
    {
       string BackgroundAssetPath { get; set; }
       Enemy[] Enemies { get; set; }
       Point EndPoint { get; set; }
       List<Point> EnemiesPath { get; set; }
    }
}
