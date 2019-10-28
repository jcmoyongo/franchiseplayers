using FP.Models;
using System;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.Buttons;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System.Diagnostics;
using System.Globalization;

namespace FP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FPMainPage : ContentPage
    {
        private readonly GameViewModel _viewModel = new GameViewModel();
        private ItemGroup<string, Game> _nextSchedule;

        //The Helper
        readonly ListViewAlternatingRowProcessor _listViewProcessor = new ListViewAlternatingRowProcessor(Color.Silver, Color.White, Color.Blue);

        private void Cell_OnAppearing(object sender, EventArgs e)
        {
            _listViewProcessor.SetBackColor(sender);
        }

        public FPMainPage()
        {
            BindingContext = _viewModel;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                _viewModel.LoadData();

                _nextSchedule = _viewModel.EarliestSchedule();

                foreach (ItemGroup<string, Game> item in ListViewGames.ItemsSource)
                    if (_nextSchedule == item)
                    {
                        ListViewGames.SelectedItem = item;
                        ListViewGames.ScrollTo(item?.FirstOrDefault(), Xamarin.Forms.ScrollToPosition.Start, true);
                        break;
                    }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void ListViewGames_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void Team_StateChanged(object sender, StateChangedEventArgs e)
        {
            var b = (sender as SfRadioButton).Text;
            //_viewModel.GroupedGames e
            //var schedule = _groupedGames.FirstOrDefault(gd => DateTime.Parse(today).CompareTo(DateTime.Parse(gd.Key)) <= 0);
            
        }

        private async void Share_Clicked(object sender, EventArgs e)
        {

            if (ListViewGames.SelectedItem != null)
            {

                Game game = ListViewGames.SelectedItem as Game;

                var groupedGames = (BindingContext as GameViewModel).GroupedGames;
                var gameGroup = groupedGames.Where(ig => ig.Key == game.GameDate).FirstOrDefault();
                StringBuilder sb = new StringBuilder();
                //sb.AppendLine($"***Franchise Players app generated test post @ { System.DateTime.Now}***");
                //sb.AppendLine();
                string winners=string.Empty;
                foreach (var g in gameGroup)
                {
                    //sb.AppendLine($"{g.Code}, {g.GameTime}, {g.Arena}");
                    Random random = new Random();
                    string winner = g.Code.Contains("MIA") ? "MIA" : random.Next(0, 1) == 0? g.HomeTeam.Code: g.AwayTeam.Code;
                    winners += $"{winner}-";
                }
                winners = winners.Substring(0, winners.Length - 1);
                sb.AppendLine(winners);
                var d = DateTime.Parse(game.GameDate);

                var frenchDate = new DateTime(d.Year, d.Month, d.Day).ToString("dddd, MMM dd, yyyy", new CultureInfo("fr-FR", false)).ToUpper();
                sb.Append(frenchDate);

                var title = frenchDate;
                var message = sb.ToString();
                _ = Xamarin.Essentials.Clipboard.SetTextAsync(message);
                await CrossShare.Current.Share(new ShareMessage { Text = message, Title = title }).ConfigureAwait(false);
                try
                {
                    Device.OpenUri(new Uri("fb://groups/franchiseplayers"));
                }
                catch
                {

                }

                //await DisplayAlert("Games", $"Games:  {}.", "OK");

                //DisplayAlert("Selected", $"Game {game.Code} selected. Grouped {groupedGames.Count}. Game date {game.GameDate}. Key = {gameGroup.Key}", "OK");

                //ItemGroup <DateTime, GameDay> gameGroup = groupedGames.Where(ig => ig.Key == game.GameDate) as ItemGroup<DateTime, GameDay>;
                //string games = string.Empty;

                //foreach (var gd in gameGroup)
                //{
                //    DisplayAlert("Group", $"Game: {gd.GameDate}", "OK");

                //    foreach (Game g in gd.Games)
                //    {
                //        games += g.Code + "-";
                //    }
                //    games.TrimEnd(new char[] { '-' });
                //}
                //Debug.Print(ListViewGames.GroupShortNameBinding.ToString());
                //DisplayAlert("Selected", $"Group . Game: {games}", "OK");
            }
        }

        private void radioGroup_CheckedChanged(object sender, Syncfusion.XForms.Buttons.CheckedChangedEventArgs e)
        {

        }

        private async void Hidden_Clicked(object sender, EventArgs e)
        {

        }
    }

    public class ListViewAlternatingRowProcessor
    {
        private bool _isEvenRow;
        readonly private Color _evenRowColor;
        readonly private Color _oddRowColor;
        readonly private Color _tappedColor;

        private ViewCell _previouslyTappedCell = null;
        private Color? _previouslyTappedCellNaturalBackColor;

        public ListViewAlternatingRowProcessor(Color evenBackColor, Color oddBackColor, Color tappedColor)
        {
            _evenRowColor = evenBackColor;
            _oddRowColor = oddBackColor;
            _tappedColor = tappedColor;
        }

        public void SetBackColor(object viewCellSender)
        {
            var viewCell = (ViewCell)viewCellSender;

            Color bg = _oddRowColor;

            viewCell.Tapped += ViewCell_Tapped;

            if (viewCell.View != null && viewCell.View.BackgroundColor == default)
            {
                if (_isEvenRow)
                {
                    bg = _evenRowColor;
                }
            }

            _isEvenRow = !_isEvenRow;

            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = bg;

            }
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            var viewCell = (ViewCell)sender;

            if (_previouslyTappedCellNaturalBackColor.HasValue)
            {
                _previouslyTappedCell.View.BackgroundColor = _previouslyTappedCellNaturalBackColor.Value;
            }

            if (viewCell.View != null)
            {
                _previouslyTappedCellNaturalBackColor = viewCell.View.BackgroundColor;
                viewCell.View.BackgroundColor = _tappedColor;

                _previouslyTappedCell = viewCell;
            }
        }
    }
}