namespace GoliathGame
{
    public static class MainStart
    {
        static void Main()
        {
            using (var gameInstance = new GoliathRun())
            {
                gameInstance.Run();
            } 
        }
    }
}
