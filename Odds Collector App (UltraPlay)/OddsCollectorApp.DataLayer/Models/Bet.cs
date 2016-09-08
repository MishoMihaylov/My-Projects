using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OddsCollectorApp.DataLayer.Models
{
    public class Bet
    {
        public Bet()
        {
            this.Odds = new HashSet<Odd>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        [ForeignKey("Match")]
        public long MatchId { get; set; }

        public Match Match { get; set; }

        public virtual ICollection<Odd> Odds { get; set; }
    }
}
