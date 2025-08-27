using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace World_Game.Model
{
    public class ResultRecordFromDatabase
    {
        public string Name { get; set; }
        public int NumberOfAttempts { get; set; }
        public string TimePlayed { get; set; }
        public string CountryName { get; set; }
    }
}
