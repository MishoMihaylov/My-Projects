using System.Collections.Generic;

namespace OddsCollectorApp.Models
{
    public class OddsMenuInformation
    {
        public OddsMenuInformation()
        {
            this.SportNames = new List<string>();
            this.EventsNames = new Dictionary<string, List<string>>();
        }

        public List<string> SportNames { get; set; }
        public Dictionary<string, List<string>> EventsNames { get; set; }
    }
}