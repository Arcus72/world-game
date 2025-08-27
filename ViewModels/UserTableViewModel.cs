using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using World_Game.Helpers;
using World_Game.Services;
using World_Game.Views;
using World_Game.Model;
using quiz.ViewModel.BaseClass;



namespace World_Game.ViewModels
{
    public class UserTableViewModel: ViewModelBase
    {
        public ObservableCollection<UserScore> UserScores { get; } = new ObservableCollection<UserScore>();

        private int _uniqueUsersCount;
        public int UniqueUsersCount
        {
            get => _uniqueUsersCount;
            set
            {
                _uniqueUsersCount = value;
                onPropertyChanged(nameof(UniqueUsersCount));
            }
        }
        public ICommand GoBackCommand { get; }

        public UserTableViewModel(NavigationService navigation)
        {
            LoadUserScores();

            // Command to navigate to the Menu
            GoBackCommand = new RelayCommand(_ => navigation.NavigateTo(new MenuView(navigation)));
        }
        private void LoadUserScores()
        {
            var records = SqlConnection.GetAllRecords();
            UniqueUsersCount = records.Select(r => r.Name).Distinct().Count();

            foreach (var record in records)
            {
                UserScores.Add(new UserScore
                {
                    PlayerName = record.Name,
                    Score = record.NumberOfAttempts,
                    TimePlayed = record.TimePlayed,
                    CountryName = record.CountryName

                });
            }
        }
    }
}
