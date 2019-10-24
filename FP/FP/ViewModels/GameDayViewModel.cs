using FP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FP.ViewModels
{
    class GameDayViewModel: BaseViewModel
    {
        public ObservableCollection<ItemGroup<DateTime, GameDay>> _GroupedGameDays;
        public ObservableCollection<ItemGroup<DateTime, GameDay>> GroupedGameDays
        {
            get { return _GroupedGameDays; }
            set
            {
                _GroupedGameDays = value;
                OnPropertyChanged(); 
            }
        }

        public void LoadData()
        {
            GameDayCollection gc = new GameDayCollection();
            List<GameDay> schedule = gc.GetGames();

            var sortedGames = from fixture in schedule
                              orderby fixture.GameDate
                              group fixture by fixture.GameDate into fixtureGroup
                              select new ItemGroup<DateTime, GameDay>(fixtureGroup.Key, fixtureGroup);

            GroupedGameDays = new ObservableCollection<ItemGroup<DateTime, GameDay>>(sortedGames);
        }

    }
}
