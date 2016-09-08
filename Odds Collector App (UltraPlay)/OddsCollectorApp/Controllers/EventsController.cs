using System;
using System.Linq;
using System.Web.Http;
using OddsCollectorApp.DataLayer;

namespace OddsCollectorApp.Controllers
{
    [RoutePrefix("api/events")]
    public class EventsController : ApiController
    {
        [Route("EventMatchesFullInfo")]
        public IHttpActionResult GetEventMatchesFullInfo(string eventId)
        {
            OddsCollectorDbContext db = new OddsCollectorDbContext();

            if(!db.Events.Where(e => e.Id == eventId).Any())
            {
                return this.BadRequest("Event does not exist!");
            }

            var startDate = DateTime.Now.AddHours(-3);
            var endDate = DateTime.Now.AddDays(1);

            var eventInfo = db.Events
                .Where(e => e.Id == eventId)
                .Single()
                .Matches
                .Where(m => m.StartDate >= startDate && m.StartDate <= endDate)
                .Select(m => new
                {
                    Id = m.Id,
                    Name = m.Name,
                    StartDate = m.StartDate.ToString(),
                    MatchType = m.MatchType,
                    Bets = m.Bets.Select(b => new
                    {
                        Name = b.Name,
                        IsLive = b.IsLive,
                        Odds = b.Odds.Select(o => new
                        {
                            Name = o.Name,
                            Value = o.Value,
                            SpecialBetValue = o.SpecialBetValue
                        })
                    })
                })
                .ToList();

            return this.Ok(eventInfo);
        }
    }
}
