using FP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace FP.Models
{
    class GameViewModel : BaseViewModel
    {
        public ObservableCollection<ItemGroup<string, Game>> _groupedGames;
        public ObservableCollection<ItemGroup<string, Game>> GroupedGames
        {
            get { return _groupedGames; }
            set
            {
                _groupedGames = value;
                OnPropertyChanged();
            }
        }

        public void LoadData()
        {
            GameCollection gc = new GameCollection();
            List<Game> schedule = gc.GetGames();

            var sortedGames = from fixture in schedule
                              orderby DateTime.Parse(fixture.GameDate)
                              group fixture by fixture.GameDate into fixtureGroup
                              select new ItemGroup<string, Game>(fixtureGroup.Key, fixtureGroup);

            GroupedGames = new ObservableCollection<ItemGroup<string, Game>>(sortedGames);
        }

        public ItemGroup<string, Game> EarliestSchedule()
        {
            if (_groupedGames == null) return null;

            var culture = new System.Globalization.CultureInfo("en-US", false);
            var today = DateTime.Now.ToString("dddd, MMM dd, yyyy", culture);

            var schedule = _groupedGames.FirstOrDefault(gd => DateTime.Parse(today).CompareTo(DateTime.Parse(gd.Key)) <= 0);
            return schedule;
        }

        public ItemGroup<string, Game> _currentItem;
        public ItemGroup<string, Game> CurrentItem
        {
            get 
            { 
                return _currentItem; 
            }
            set
            {
                _currentItem = value;
                OnPropertyChanged();
            }
        }

        //public ICommand ItemClickCommand
        //{
        //    get
        //    {
        //        return new Command((item) =>
        //        {
        //            CurrentItem = item as ItemGroup<string, Game>;
        //        });
        //    }
        //}
    }
  
}

