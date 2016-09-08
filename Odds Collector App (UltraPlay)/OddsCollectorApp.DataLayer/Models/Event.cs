using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OddsCollectorApp.DataLayer.Models
{
    public class Event
    {
        public Event()
        {
            this.Matches = new HashSet<Match>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public long CategoryId { get; set; }
        
        public string Name { get; set; }

        public bool IsLive { get; set; }

        [ForeignKey("Sport")]
        public int SportId { get; set; }

        public Sport Sport { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
