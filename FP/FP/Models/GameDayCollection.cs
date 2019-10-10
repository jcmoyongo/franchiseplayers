using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;

namespace FP.Models
{
    public class GameDayCollection
    {
        private List<GameDay> _schedule;

        public List<GameDay> Schedule { get => _schedule; private set => _schedule = value; }

        public GameDayCollection()
        {
            Schedule = new List<GameDay>();
        }

        public List<GameDay> GetGames()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(TeamCollection)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("FP.Data.Schedule1819.csv");


                using (StreamReader sr = new StreamReader(stream))
                {
                    DateTime gameDate;
                    GameDay gameDay = new GameDay(DateTime.MinValue);//??
                    List<Team> teams = TeamCollection.GetTeams();

                    string line = string.Empty;
                    Schedule = new List<GameDay>();
                    //First line is the date, second line is the header for the game table. Skip both lines at the beginning of each game day.
                    while ((line = sr.ReadLine()) != null)
                    {
                        //First line should look like "Tuesday, Oct 16, 2018",,,,, This from text copied from https://www.cbssports.com/nba/schedules
                        if (IsGameDate(line, out gameDate))
                        {
                            gameDay = new GameDay(gameDate);
                            Schedule.Add(gameDay);
                            //Skip the header -- Away,Home,Time,Venue,NATL TV,Tickets
                            sr.ReadLine();
                        }
                        else
                        {
                            try
                            {
                                var game = line.Split(new char[] { ',' });
                                Team team1 = teams.Find(t => t.CityName == game[0] || t.CityName == game[0]);
                                Team team2 = teams.Find(t => t.CityName == game[1]);
                                var gameTime = DateTime.Parse(gameDay.GameDate.ToShortDateString() + " " + game[2]);
                                //gameDay.Games.Add(new Game($"{team1.Code}-{team2.Code}", team1, team2, gameTime));
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    return Schedule;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool IsGameDate(string line, out DateTime gameDate)
        {
            var firsLine = line.Split(new char[] { ',' });
            string gameDateString = (firsLine[0] + "," + firsLine[1] + firsLine[2]).Replace(@"""", "");
            return DateTime.TryParse(gameDateString, out gameDate);
        }
    }
}
