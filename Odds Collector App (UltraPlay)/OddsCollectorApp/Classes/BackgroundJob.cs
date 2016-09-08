using OddsCollectorApp.DataLayer;
using OddsCollectorApp.DataLayer.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;

namespace OddsCollectorApp.Classes
{
    public class BackgroundJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            string timeElapsed = this.Feed();

            using (StreamWriter writer = new StreamWriter(@"D:\updateLog.txt", true))
            {
                writer.WriteLine("Update on: " + DateTime.Now + ". Update elapsed:" + timeElapsed + ".");
            }
        }

        public string Feed()
        {
            Stopwatch stopwatch = new Stopwatch();

            string URLString = "http://vitalbet.net/sportxml";
            OddsCollectorDbContext db = new OddsCollectorDbContext();
            //db.Sports.Add(new Sport()
            //{
            //    Id = 1,
            //    Name = "aa"
            //});

            HttpWebRequest request = WebRequest.Create(URLString) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return "Xml feed not available atm";
            }
            else
            {
                stopwatch.Start();

                XmlTextReader xmlReader = new XmlTextReader(response.GetResponseStream());

                int lastSportId = 0;
                string lastEventId = null;
                long lastMatchId = 0;
                long lastBetId = 0;

                List<Sport> sports = new List<Sport>();
                List<Event> events = new List<Event>();
                List<Match> matches = new List<Match>();
                List<Bet> bets = new List<Bet>();
                List<Odd> odds = new List<Odd>();

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.IsStartElement())
                    {
                        switch (xmlReader.Name)
                        {
                            case "Sport":
                                lastSportId = int.Parse(xmlReader.GetAttribute("ID"));
                                string sportName = xmlReader.GetAttribute("Name");

                                Sport lastAddedSport = new Sport()
                                {
                                    Id = lastSportId,
                                    Name = sportName
                                };

                                sports.Add(lastAddedSport);
                                break;
                            case "Event":
                                if(xmlReader.GetAttribute("ID") == "0")
                                {
                                    lastEventId = xmlReader.GetAttribute("ID") + xmlReader.GetAttribute("CategoryID");
                                }
                                else
                                {
                                    lastEventId = xmlReader.GetAttribute("ID");
                                }
                                string eventName = xmlReader.GetAttribute("Name");
                                long eventCategoryId = long.Parse(xmlReader.GetAttribute("CategoryID"));
                                bool eventIsLive = xmlReader.GetAttribute("IsLive").ToLower() == "false" ? false : true;

                                Event lastAddedEvent = new Event()
                                {
                                    Id = lastEventId,
                                    Name = eventName,
                                    CategoryId = eventCategoryId,
                                    IsLive = eventIsLive,
                                    SportId = lastSportId
                                };

                                events.Add(lastAddedEvent);
                                break;
                            case "Match":
                                lastMatchId = long.Parse(xmlReader.GetAttribute("ID"));
                                string matchName = xmlReader.GetAttribute("Name");
                                DateTime matchStartDate = DateTime.Parse(xmlReader.GetAttribute("StartDate"));
                                matchStartDate = matchStartDate.AddHours(3);
                                string matchType = xmlReader.GetAttribute("MatchType");

                                Match lastAddedMatch = new Match()
                                {
                                    Id = lastMatchId,
                                    Name = matchName,
                                    StartDate = matchStartDate,
                                    MatchType = matchType,
                                    EventId = lastEventId
                                };

                                matches.Add(lastAddedMatch);
                                break;
                            case "Bet":
                                lastBetId = long.Parse(xmlReader.GetAttribute("ID"));
                                string betName = xmlReader.GetAttribute("Name");
                                bool betIsLive = xmlReader.GetAttribute("IsLive").ToLower() == "false" ? false : true;

                                Bet lastAddedBet = new Bet()
                                {
                                    Id = lastBetId,
                                    Name = betName,
                                    IsLive = betIsLive,
                                    MatchId = lastMatchId
                                };

                                bets.Add(lastAddedBet);
                                break;
                            case "Odd":
                                long oddId = long.Parse(xmlReader.GetAttribute("ID"));
                                string oddName = xmlReader.GetAttribute("Name");
                                float oddValue = float.Parse(xmlReader.GetAttribute("Value"));
                                string oddSpecialBetValue = null;

                                if (xmlReader.GetAttribute("SpecialBetValue") != null)
                                {
                                    oddSpecialBetValue = xmlReader.GetAttribute("SpecialBetValue").ToString();
                                }

                                Odd newOdd = new Odd()
                                {
                                    Id = oddId,
                                    Name = oddName,
                                    Value = oddValue,
                                    SpecialBetValue = oddSpecialBetValue,
                                    BetId = lastBetId
                                };

                                odds.Add(newOdd);
                                break;
                        }
                    }
                }

                db.BulkMerge(sports);
                db.BulkMerge(events);
                db.BulkMerge(matches);
                db.BulkMerge(bets);
                db.BulkMerge(odds);
                stopwatch.Stop();

                return stopwatch.Elapsed.ToString();
            }            
        }
    }
}