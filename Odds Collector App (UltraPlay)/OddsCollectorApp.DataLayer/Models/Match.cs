using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OddsCollectorApp.DataLayer.Models
{
    public class Match
    {
        public Match()
        {
            this.Bets = new HashSet<Bet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        [ForeignKey("Event")]
        public string EventId { get; set; }

        public Event Event { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
