using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OddsCollectorApp.DataLayer.Models
{
    public class Sport
    {
        public Sport()
        {
            this.Events = new HashSet<Event>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
