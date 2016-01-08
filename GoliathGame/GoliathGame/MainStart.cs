using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace GoliathGame
{   
    public static class MainStart
    {
        static void Main()
        {
            //The game is still in development
            //Attack system is working but need fixes on the animation and updates
            //Items are still to be made
            //Levels are still to be made
            //Enemies animations are still to be made

            using (var game = new GoliathRun())
            {
                game.Run();
            } 
        }
    }
}
