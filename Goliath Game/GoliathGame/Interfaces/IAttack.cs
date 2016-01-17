using System;
using Microsoft.Xna.Framework;

namespace GoliathGame.Interfaces
{
    public interface IAttack
    {
        int AttackDamage { get; set; }
        int AttackingRange { get; set; }
        double AttackSpeedDelay { get; set; }
        void Strike(IUnit targetUnit, GameTime theGameTime);
        TimeSpan IntervalBetweenAttack { get; set; }
    }
}
