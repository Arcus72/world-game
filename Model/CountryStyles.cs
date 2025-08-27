using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace World_Game.Model
{
    public class CountryStyles
    {
        BitmapImage arrowDownImage = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/arrow_down.png")));
        BitmapImage arrowUpImage = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/arrow_up.png")));
        BitmapImage yesImage = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/yes.png")));
        BitmapImage noImage = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/no.png")));
        BitmapImage noneImage = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/none.png")));
        BitmapImage yellowImage = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/yellow.png")));


        public BitmapImage Population { get; set; }
        public BitmapImage SurfaceArea { get; set; }
        public BitmapImage Continent { get; set; }
        public BitmapImage GovernmentForm { get; set; }
        public BitmapImage Region { get; set; }
        public BitmapImage IndepYear { get; set; }
        public BitmapImage Language { get; set; }


        public CountryStyles(string populationType = "none",
                     string surfaceAreaType = "none",
                     string continentType = "none",
                     string governmentFormType = "none",
                     string regionType = "none",
                     string indepYearType = "none",
                     string languageType = "none")
        {
            Population = matchImage(populationType);
            SurfaceArea = matchImage(surfaceAreaType);
            Continent = matchImage(continentType);
            GovernmentForm = matchImage(governmentFormType);
            Region = matchImage(regionType);
            IndepYear = matchImage(indepYearType);
            Language = matchImage(languageType);
        }

    
        private BitmapImage matchImage(string type)
        {
            return type switch
            {
                "arrowDown" => arrowDownImage,
                "arrowUp" => arrowUpImage,
                "yes" => yesImage,
                "no" => noImage,
                "yellow" => yellowImage,
                _ => noneImage,
            };
        }
    }
}
