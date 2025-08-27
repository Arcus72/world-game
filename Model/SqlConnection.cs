using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace World_Game.Model
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Xml.Linq;
    using MySql.Data.MySqlClient;

   public class SqlConnection
    {

        const string server = "localhost";
        const string user = "root";
        const string database = "world";
        const string port = "3306";
        const string password = "";

        static public CountryData GetCountryData(string countryName)
        {
       

            string connectionString = $"server={server};user={user};database={database};port={port};password={password}";


            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = " SELECT Population, IndepYear, Region, SurfaceArea, GovernmentForm, Continent, Language, IsOfficial FROM `country` join countrylanguage on country.Code = countrylanguage.CountryCode WHERE  Name = @name;";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", countryName.Trim()); 

                    using (var reader = command.ExecuteReader())
                    {
                        
                        if (reader.Read())
                        {
                            int population = 0;
                            int.TryParse(reader["Population"]?.ToString(), out population);

                            int indepYear = 0;
                            int.TryParse(reader["IndepYear"]?.ToString(), out indepYear);

                            int surfaceArea = 0;
                            int.TryParse(reader["SurfaceArea"]?.ToString(), out surfaceArea);
                            string region = reader["Region"].ToString();
                            string governmentForm = reader["GovernmentForm"].ToString();
                            CountryData newContry = new CountryData(countryName, population, surfaceArea, region, governmentForm, indepYear);

                            
                            do{    
                                newContry.Continent = reader["Continent"].ToString();
                                if (reader["IsOfficial"].ToString() == "T")
                                    newContry.Language = reader["Language"].ToString();

                            } while (reader.Read()) ;

                             return newContry;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        static public List<string> GetAllCountryNames()
        {
            List<string> countryNames = new List<string>();

            string connectionString = "server=localhost;user=root;database=world;port=3306;password=";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Name FROM country;";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["Name"].ToString();
                            countryNames.Add(name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return countryNames;
        }

        static public void SaveRecord(string Name, string NumberOfAttempts, string time, string countryName)
        {
            string connectionString = "server=localhost;user=root;database=world;port=3306;password=";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 1. Add the user if they don't exist
                    string insertUser = "INSERT IGNORE INTO users (name) VALUES (@Name);";
                    using (var insertCmd = new MySqlCommand(insertUser, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@Name", Name);
                        insertCmd.ExecuteNonQuery();
                    }

                    // 2. Retrieve user_id
                    string selectUserId = "SELECT id FROM users WHERE name = @Name;";
                    int userId;
                    using (var selectCmd = new MySqlCommand(selectUserId, connection))
                    {
                        selectCmd.Parameters.AddWithValue("@Name", Name);
                        using (var reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                                userId = reader.GetInt32("id");
                            else
                                throw new Exception("Nie znaleziono użytkownika.");
                        }
                    }

                    // 3. Add a record to scores
                    string insertScore = "INSERT INTO scores (user_id, numberOfAttempts, timePlayed, countryName) " +
                                         "VALUES (@UserId, @NumberOfAttempts, @Time, @CountryName);";
                    using (var scoreCmd = new MySqlCommand(insertScore, connection))
                    {
                        scoreCmd.Parameters.AddWithValue("@UserId", userId);
                        scoreCmd.Parameters.AddWithValue("@NumberOfAttempts", NumberOfAttempts);
                        scoreCmd.Parameters.AddWithValue("@Time", time);
                        scoreCmd.Parameters.AddWithValue("@CountryName", countryName);
                        scoreCmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nie udało się połączyć z bazą danych: " + ex.Message);
                }
            }
        }


        public static List<ResultRecordFromDatabase> GetAllRecords()
        {
            List<ResultRecordFromDatabase> records = new List<ResultRecordFromDatabase>();

            string connectionString = "server=localhost;user=root;database=world;port=3306;password=";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT name, numberOfAttempts, timePlayed, countryName FROM users join scores on users.id = user_id order by timePlayed; ";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new ResultRecordFromDatabase
                            {
                                Name = reader["name"].ToString(),
                                NumberOfAttempts = Convert.ToInt32(reader["numberOfAttempts"]),
                                TimePlayed = reader["timePlayed"].ToString(),
                                CountryName = reader["countryName"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas pobierania danych: " + ex.Message);
                    return new List<ResultRecordFromDatabase>(); 
                }
            }

            return records;
        }

    }
}
