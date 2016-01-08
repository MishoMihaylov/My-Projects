using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GoliathGame.Interfaces.EngineInterfaces;

namespace GoliathGame.Interfaces
{
    interface IUnit : IAttack, IDefence, IPosition, IMovable, IObject
    {
        int Health { get; set; }
        Rectangle Rectangle { get; set; }
        bool IsDead();
        void Die();
    }
}
