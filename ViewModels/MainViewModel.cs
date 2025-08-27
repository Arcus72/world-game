using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using World_Game.Helpers;
using World_Game.Views;
using World_Game.Services;


namespace World_Game.ViewModels
{
    public class MainViewModel
    {
        public NavigationService Navigation { get; } = new World_Game.Services.NavigationService();

        public ICommand ShowMenuCommand { get; }
        public ICommand ShowGameCommand { get; }
        public ICommand ShowScoresCommand { get; }

        public MainViewModel()
        {
            ShowMenuCommand = new RelayCommand(_ => Navigation.NavigateTo(new MenuView(Navigation)));
            ShowGameCommand = new RelayCommand(_ => Navigation.NavigateTo(new GameView(Navigation)));
            ShowScoresCommand = new RelayCommand(_ => Navigation.NavigateTo(new UserTableView(Navigation)));

            // By default, display the Menu at startup
            Navigation.NavigateTo(new MenuView(Navigation));
        }
    }
}
