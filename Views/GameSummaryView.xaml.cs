using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using World_Game.ViewModels;
using World_Game.Services;
using World_Game.Model;

namespace World_Game.Views
{
    /// <summary>
    /// Logika interakcji dla klasy GameSummaryView.xaml
    /// </summary>
    public partial class GameSummaryView : UserControl
    {
        public GameSummaryView(NavigationService navigation, int attempts, string time, CountryData correctCountry)
        {
            InitializeComponent();
            DataContext = new GameSummaryViewModel(navigation, attempts, time, correctCountry);
        }

        private void CountryGuessControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
