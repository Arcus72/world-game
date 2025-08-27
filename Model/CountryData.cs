using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace World_Game.Model
{
    public class CountryData
    {
        public CountryData(string name, int population, int surfaceArea, string region, string governmentForm, int indepYear)
        {
            Name = name;
            Population = population;
            SurfaceArea = surfaceArea;
            GovernmentForm = governmentForm;
            Region = region;
            IndepYear = indepYear;

        }

        public string PopulationDirectionIcon { get; set; }

        public string Name { get; set; }
 
        public int Population { get ; set; }

        public int SurfaceArea { get; set; }

        public int IndepYear { get; set; }

        public string GovernmentForm { get; set; }

        public string Region { get; set; }

        private List<string> _continents = [];
        public string Continent { get => string.Join("\n", _continents);

            set
            {
                if (!_continents.Contains(value))
                {
                    _continents.Add(value);
                }
              
            }
        }

        private List<string> _language = [];
        public string Language
        {
            get => string.Join("\n", _language);

            set
            {
                if (!_language.Contains(value))
                {
                    _language.Add(value);
                }

            }
        }

        public CountryStyles Styles { get; set; } = null;


        public string CompareLists(List<string> list1, string listName)
        {
            List<string> list2;
            if(listName == "continents")
                list2 = _continents;
           else if (listName == "language")
                list2 = _language;
            else
             return "";

            if (list1.Count == 0 || list2.Count == 0)
                return "none";

            // GREEN: contents are exactly the same (order doesn't matter)
            if (list1.Count == list2.Count && !list1.Except(list2).Any() && !list2.Except(list1).Any())
                return "yes";

            // YELLOW: at least one common element
            if (list1.Intersect(list2).Any())
                return "yellow";

            // RED: no common elements
            return "no";
        }

        private static string _compareNumeric(int a, int b)
        {
            if (a == 0 || b == 0)
                return "none";
            return a > b ? "arrowDown" : a < b ? "arrowUp" : "yes";
        }


        public void CompereToCountry(CountryData selectedCountry)
        {

            string continentType = selectedCountry.CompareLists(_continents, "continents");
            string languageType = selectedCountry.CompareLists(_language, "language");


            string populationType = _compareNumeric(Population, selectedCountry.Population);
            string surfaceAreaType = _compareNumeric(SurfaceArea, selectedCountry.SurfaceArea);
            string indepYearType = _compareNumeric(IndepYear, selectedCountry.IndepYear);

            string governmentFormType = GovernmentForm == selectedCountry.GovernmentForm ? "yes" : "no";
            string regionType = Region == selectedCountry.Region ? "yes" : "no";

            Styles = new CountryStyles(populationType,
                     surfaceAreaType,
                     continentType,
                     governmentFormType,
                     regionType,
                    indepYearType,
                     languageType);

        }
      
        
    }
}
