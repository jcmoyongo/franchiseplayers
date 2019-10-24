using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace FP.Models
{
    public class GameCollection
    {
        private List<Game> _schedule;

        public List<Game> Schedule { get => _schedule; private set => _schedule = value; }

        public GameCollection()
        {
            Schedule = new List<Game>();
        }

        public List<Game> GetGamesOld()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(TeamCollection)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("FP.Data.Schedule1920.csv");


                using (StreamReader sr = new StreamReader(stream))
                {
                    string currentGameDate = DateTime.MinValue.ToString(new CultureInfo("en-US", false));
                    //Game Game = new Game(DateTime.MinValue);//??
                    List<Team> teams = TeamCollection.GetTeams();

                    string line = string.Empty;
                    Schedule = new List<Game>();
                    //First line is the date, second line is the header for the game table. Skip both lines at the beginning of each game day.
                    while ((line = sr.ReadLine()) != null)
                    {
                        //First line should look like "Tuesday, Oct 16, 2018",,,,, This from text copied from https://www.cbssports.com/nba/schedules
                        if (IsGameDateLine(line, out DateTime gameDate))
                        {
                            var date = line.Split(new char[] { ',' });
                            currentGameDate = $"{date[0]},{date[1]},{date[2]}".Replace(@"""", "").ToUpper(new CultureInfo("en-US", false));
                            //Skip to the header -- Away,Home,Time,Venue,NATL TV,Tickets
                            sr.ReadLine();
                        }
                        else
                        {
                            try
                            {
                                var game = line.Split(new char[] { ',' }); //i.e. Denver,Portland,10:30 PM,Moda Center,ESPN,Tickets starting at $19.86
                                Team awayTeam = teams.Find(t => t.ShortName == game[0] || t.ShortName == game[0]);
                                Team homeTeam = teams.Find(t => t.ShortName == game[1]);
                                //var gameTime = DateTime.Parse(currentGameDate.ToShortDateString() + " " + game[2]);
                                string gameCode = $"{awayTeam.Code}-{homeTeam.Code}";
                                string time = game[2];
                                Schedule.Add(new Game(gameCode, awayTeam, homeTeam, currentGameDate, time));
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    }
                    return Schedule;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Game> GetGames()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(TeamCollection)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("FP.Data.Schedule1920.csv");


                using (StreamReader sr = new StreamReader(stream))
                {
                    List<Team> teams = TeamCollection.GetTeams();

                    string line = string.Empty;
                    Schedule = new List<Game>();
                    //First line is the header for the game table. Skip it.
                    sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                            try
                            {
                                var culture = new CultureInfo("en-US", false);
                                var game = line.Split(new char[] { ',' }); //i.e. 10/22,8:00 PM,8:00 PM,3+,New Orleans Pelicans,Toronto Raptors,3+,Scotiabank Arena
                                Team awayTeam = teams.Find(t => t.FullName == game[4]);
                                Team homeTeam = teams.Find(t => t.FullName == game[5]);
                                string gameCode = $"{awayTeam.Code}-{homeTeam.Code}";
                                string time = game[1];
                                var dd = game[0].Split(new char[] { '/' }); 
                                int month = int.Parse(dd[0], culture);
                                int day = int.Parse(dd[1], culture);
                                int year = month >= 10 & month <= 12 ? 2019 : 2020;
                                string currentGameDate = new DateTime(year, month, day).ToString("dddd, MMM dd, yyyy", culture);
                                Schedule.Add(new Game(gameCode, awayTeam, homeTeam, currentGameDate.ToUpper(culture), time, arena:game[7]));
                        }
                            catch (Exception ex)
                            {
                                throw;
                            }
                    }
                    return Schedule;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static bool IsGameDateLine(string line, out DateTime gameDate)
        {
            var firsLine = line.Split(new char[] { ',' });
            string gameDateString = (firsLine[0] + "," + firsLine[1] + firsLine[2]).Replace(@"""", "");
            bool isGameDate = DateTime.TryParse(gameDateString, out gameDate);
            return isGameDate;// && DateTime.Compare(gameDate, DateTime.Now) >= 0; //Only games as of today.
        }
    }
}
