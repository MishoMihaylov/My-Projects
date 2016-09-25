using System;
using System.Linq;
using System.Web.Http;
using OddsCollectorApp.DataLayer;

namespace OddsCollectorApp.Api
{
    [RoutePrefix("api/sports")]
    public class SportsController : ApiController
    {
        [Route("AllSportNames")]
        public IHttpActionResult GetSports()
        {
            OddsCollectorDbContext db = new OddsCollectorDbContext();

            var sports = db.Sports
                .Where(s => s.Name != null)
                .Select(s => s.Name)
                .ToList();

            return this.Ok(sports);
        }

        [Route("SportsWithTheirEvents")]
        public IHttpActionResult GetSportsWithEvents()
        {
            OddsCollectorDbContext db = new OddsCollectorDbContext();

            var startDate = DateTime.Now.AddHours(-3);
            var endDate = DateTime.Now.AddDays(1);

            var sportsWithTheirEvents = db.Sports
                .Where(s => s.Name != null)
                .Where(s => s.Events.Count > 0)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    Events = s.Events
                        .Where(e => e.Name != null)
                        .Where(e => e.Matches
                                    .Where(m => m.StartDate >= startDate && m.StartDate <= endDate)
                                    .Count() > 0)
                        .Where(e => e.Matches.Count > 0)
                        .Select(e => new
                        { 
                            Id = e.Id,
                            Name = e.Name,
                            IsLive = e.IsLive
                        })
                })
                .ToList();

            sportsWithTheirEvents = sportsWithTheirEvents.Where(s => s.Events.Count() > 0).ToList();

            return this.Ok(sportsWithTheirEvents);
        }

        [Route("SportsFullInfo")]
        public IHttpActionResult GetSportsFullInformation()
        {
            OddsCollectorDbContext db = new OddsCollectorDbContext();

            var sports = db.Sports
                .Select(s => new
                {
                    Name = s.Name,
                    Events = s.Events.Select(e => new
                    {
                        Name = e.Name,
                        IsLive = e.IsLive,
                        Matches = e.Matches.Select(m => new
                        {
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
                    })
                })
                .ToList();

            return this.Ok(sports);
        }
    }
}
