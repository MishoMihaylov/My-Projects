using System;

namespace TowerDefence.Models.Unilities
{
    public static class Validator
    {
        public static void CheckIfNegativeOrZero(int value, string checkFor)
        {
            if(value <= 0)
            {
                throw new ArgumentOutOfRangeException(checkFor + " cannot be negative or zero.");
            }
        }

        public static bool IsInRange(int value, int from, int to)
        {
            if(value <= to && value >= from)
            {
                return true;
            }

            return false;
        }
    }
}
