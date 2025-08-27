# 🌍 Country Guessing Game

A country guessing game written in C#.
The goal is to guess the randomly selected country based on hints (e.g. continent, population, language).

---

## Features

-  Random country selection from the database.
-  Attempt counter and game timer.
-  Hints (continent, population, capital).
-  Scoreboard (saves player results).

---

## Controls

-  In the start menu, choose the option **"Start Game"**.
-  Enter the country name in the answer field.
-  Click **Check** to confirm your answer.
-  After the game ends, a summary is displayed with an option to save the result.

---

## Technologies

-  **Language:** C#
-  **Extensions:** MySql.Data, MaterialDesignThemes
-  **Database:** MySQL (XAMPP)
-  **IDE:** Visual Studio 2022

---

## Installation and Setup

1. Install Visual Studio 2022 and XAMPP.
2. Start the MySQL server.
3. Import the `world.sql` database.
4. Open the project in Visual Studio and run the game.

---

## Database connection

Values like server name, user, database, port, or password can be changed in the file `Model/SqlConnection.cs` (lines 20–24).

## Database

The database structure can be found in the **world.sql** file.
This file contains the definition of all required tables.

### Project tables

-  **country** – contains basic information about countries, such as code (Code), name (Name), Continent, Region, SurfaceArea, IndepYear (year of independence), Population, and GovernmentForm.

-  **countrylanguage** – stores information about languages spoken in each country. Includes the country code (CountryCode), language name (Language), whether it is an official language (IsOfficial), and the percentage of the population speaking it (Percentage).

-  **scores** – stores user results in the game or quiz. Includes record ID (id), user ID (user_id), number of attempts (numberOfAttempts), playtime (timePlayed), and the country name related to the result (countryName).

-  **users** – stores user data. Includes ID (id) and the user’s name (name).

---

## License

This project is licensed under the MIT License.
