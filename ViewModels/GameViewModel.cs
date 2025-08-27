using quiz.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using World_Game.Helpers;
using World_Game.Model;
using World_Game.Services;
using World_Game.ViewModels.utils;
using World_Game.Views;


namespace World_Game.ViewModels
{
    public class GameViewModel:  ViewModelBase
    {
        private readonly NavigationService _navigation;
        public ICommand CheckCountryCommand { get; }
        public ICommand ChangeMapVisibilityStatusCommand { get; }

        public TimerViewModel Timer { get; } = new TimerViewModel();

        public string MapStatusBtnText { get; set; } = "Show map"; 
        public string MapVisibilityStatus { get; set;  } = "Visible"; 

        private string _countryInputText;
        public string CountryInputText
        {
            get => _countryInputText;
            set
            {
                _countryInputText = value;
                onPropertyChanged(nameof(CountryInputText)); 
            }
        }

        public GameModel Game { get; } = new GameModel();

        public GameViewModel(NavigationService navigation)
        {
            CheckCountryCommand = new RelayCommand(_checkCountry);
            ChangeMapVisibilityStatusCommand = new RelayCommand(_changeMapVisibilityStatus);
            _navigation = navigation;
            Timer.Start(); 
          
        }

        private void _changeMapVisibilityStatus(object parameter)
        {
            if (MapVisibilityStatus == "Visible")
            {
                MapVisibilityStatus = "Hidden";
                MapStatusBtnText = "Hide map";
            }
            else
            {
                MapVisibilityStatus = "Visible";
                MapStatusBtnText = "Show map";
            }
                
            onPropertyChanged(nameof(MapVisibilityStatus));
            onPropertyChanged(nameof(MapStatusBtnText));
        }

        private void _checkCountry(object parameter)
        {
            string country = parameter?.ToString() ?? "";
            if (Game.SelectedCountry.Name == country)
            {
                Timer.Stop();
                _navigation.NavigateTo(new GameSummaryView(_navigation, Game.Round, Timer.ElapsedTime, Game.SelectedCountry)); // Parameters passed to Summary
            }
            Game.EnterNewAttempt(country);
            
            CountryInputText = ""; 
        }
    }
}

