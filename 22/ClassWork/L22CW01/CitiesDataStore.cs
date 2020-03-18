using L22CW01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L22CW01
{
    public class CitiesDataStore
    {
        private static CitiesDataStore _instance = new CitiesDataStore();
        public static CitiesDataStore Instance { get => _instance; }
        public List<City> Cities { get; private set; }

        private CitiesDataStore()
        {
            Cities = new List<City>()
            {
                new City(1, "Moscow"),
                new City(2, "Saint-Petersburg"),
                new City(3,"Montreal")
            };
        }
    }
}
