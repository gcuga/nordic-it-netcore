using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L22CW01.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public City()
        {
        }

        public City(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
