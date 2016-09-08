using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OddsCollectorApp.DataLayer.Models
{
    public class Odd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public long XmlId { get; set; }

        public string Name { get; set; }

        public float Value { get; set; }

        [ForeignKey("Bet")]
        public long BetId { get; set; }

        public Bet Bet { get; set; }

        public string SpecialBetValue { get; set; }

    }
}
