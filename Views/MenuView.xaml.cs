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

namespace World_Game.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView(NavigationService navigation)
        {
            InitializeComponent();
            DataContext = new MenuViewModel(navigation);
        }
    }
}
