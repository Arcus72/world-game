using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



namespace World_Game.Model
{
    public class GameModel
    {
        
        public int Round { get; set; }

        public CountryData SelectedCountry { get; private set; }

        public ObservableCollection<string> CountryNames { get; } = new ObservableCollection<string>();
        
        public ObservableCollection<CountryData> GuessHistory { get; } = new ObservableCollection<CountryData>();

        private string _getRandomElement(List<string> list)
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("The list of countries cannot be empty.");

            Random random = new Random();
            int index = random.Next(list.Count);  

            return list[index];
        }

        private void _initializeCountries()
        {
            var countries = SqlConnection.GetAllCountryNames();
            foreach (var country in countries)
                CountryNames.Add(country);
        }


        private void _selectRandomCountry()
        {
            int errorCount = 0;
            const int MaxRetries = 3;

            while (errorCount < MaxRetries)
            {
                string randomCountryName = _getRandomElement(CountryNames.ToList());
                SelectedCountry = SqlConnection.GetCountryData(randomCountryName);

                if (SelectedCountry != null)
                {
                    SelectedCountry.CompereToCountry(SelectedCountry); 
                    return;
                }

                errorCount++;
            }

            MessageBox.Show("Failed to retrieve country data.");
        }

        public GameModel()
        {
            Round = 1;
            _initializeCountries();
            _selectRandomCountry();
        }

        public void EnterNewAttempt(string countryName)
        {

            CountryData newCountry = SqlConnection.GetCountryData(countryName);
            if (newCountry == null)
            {
                MessageBox.Show("Could not fetch country data.");
                return;
            }
            newCountry.CompereToCountry(SelectedCountry);
            GuessHistory.Add(newCountry);
            Round++;

        }
        
    }
}
