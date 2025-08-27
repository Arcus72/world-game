using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using World_Game.Helpers;
using World_Game.Model;
using World_Game.Services;
using World_Game.Views;


namespace World_Game.ViewModels
{
    public class GameSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CountryData CorrectCountry { get; set; }

        private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public int AttemptsCount { get; set; }

        public string TimePlayed { get; set; }

        private string _playerName;

        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                OnPropertyChanged(nameof(PlayerName));
            }
        }

        public ICommand GoToMenuCommand { get; }
        public ICommand GoToScoresCommand { get; }

        public GameSummaryViewModel(NavigationService navigation, int attempts, string time, CountryData correctCountry)
        {
            AttemptsCount = attempts;
            TimePlayed = time.Substring(6);

            GoToMenuCommand = new RelayCommand(_ => navigation.NavigateTo(new MenuView(navigation)));
            GoToScoresCommand = new RelayCommand(_ => ExecuteGoToScores(navigation));
            CorrectCountry = correctCountry;
        }

        private void ExecuteGoToScores(NavigationService navigation)
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                MessageBox.Show("Please enter your player name.");
                return;
            }
            SqlConnection.SaveRecord(PlayerName, AttemptsCount.ToString(), TimePlayed, CorrectCountry.Name);
            MessageBox.Show("Score saved!");
            navigation.NavigateTo(new UserTableView(navigation));
        }
    }
}
