using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using World_Game.Helpers;
using World_Game.Services;
using World_Game.Views;


namespace World_Game.ViewModels
{
    public class MenuViewModel
    {
        public ICommand ShowGameCommand { get; }
        public ICommand ShowScoresCommand { get; }
        public ICommand ExitCommand { get; }

        public NavigationService Navigation { get; }

        public MenuViewModel(NavigationService navigation)
        {
            Navigation = navigation;

            ShowGameCommand = new RelayCommand(_ => Navigation.NavigateTo(new GameView(Navigation)));
            ShowScoresCommand = new RelayCommand(_ => Navigation.NavigateTo(new UserTableView(Navigation)));
            ExitCommand = new RelayCommand(_ => System.Windows.Application.Current.Shutdown());
        }
    }
}
