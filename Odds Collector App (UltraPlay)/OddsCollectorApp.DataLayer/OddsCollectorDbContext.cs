namespace OddsCollectorApp.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using OddsCollectorApp.DataLayer.Models;

    public class OddsCollectorDbContext : DbContext
    {
        // Your context has been configured to use a 'OddsCollectorDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'OddsCollectorApp.DataLayer.OddsCollectorDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'OddsCollectorDbContext' 
        // connection string in the application configuration file.
        public OddsCollectorDbContext()
            : base("name=OddsCollectorDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<Odd> Odds { get; set; }
    }
}