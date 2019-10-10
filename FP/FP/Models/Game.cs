using System;
using System.Collections.Generic;
using System.Text;

namespace FP.Models
{
    public class Game
    {
        public string Code { get; set; }
        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
        public string GameDate { get; private set; }
        public string GameTime { get; set; }
        public int AwayTeamScore { get; set; }
        public int HomeTeamScore { get; set; }
        public string Arena { get; set; }
        public string Winner { get; set; }


        public Game(string code, Team awayTeam, Team homeTeam, string gameDate, string gameTime, int awayTeamScore = 0, int homeTeamScore = 0, string arena = "", string winner = "")
        {
            Code = code;
            AwayTeam = awayTeam;
            HomeTeam = homeTeam;
            GameDate = gameDate;
            GameTime = gameTime;
            AwayTeamScore = awayTeamScore;
            HomeTeamScore = homeTeamScore;
            Arena = arena;
            Winner = winner;
        
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
