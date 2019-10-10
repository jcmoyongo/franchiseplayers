using System;
using System.Collections.Generic;
using System.Text;

namespace FP.Models
{
    public class GameDay
    {
        private DateTime _gameDate;
        private List<Game> _games;

        public DateTime GameDate { get => _gameDate; private set => _gameDate = value; }
        public List<Game> Games { get => _games; private set => _games = value; }

        public GameDay(DateTime gameDate)
        {
            GameDate = gameDate;
            Games = new List<Game>();
        }

    }
}
