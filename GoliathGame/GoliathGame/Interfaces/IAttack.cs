using System;
using Microsoft.Xna.Framework;

namespace GoliathGame.Interfaces
{
    interface IAttack
    {
        int AttackDamage { get; set; }
        int AttackingRange { get; set; }
        int AttackSpeedDelay { get; set; }
        void Strike(IUnit targetUnit, GameTime theGameTime);
        TimeSpan IntervalBetweenAttack { get; set; }
    }
}
