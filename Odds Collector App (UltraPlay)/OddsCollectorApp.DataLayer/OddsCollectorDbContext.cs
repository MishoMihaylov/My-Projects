namespace OddsCollectorApp.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using OddsCollectorApp.DataLayer.Models;

    public class OddsCollectorDbContext : DbContext
    {
        public OddsCollectorDbContext()
            : base("name=OddsCollectorDbContext")
        {
        }

        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<Odd> Odds { get; set; }
    }
}
