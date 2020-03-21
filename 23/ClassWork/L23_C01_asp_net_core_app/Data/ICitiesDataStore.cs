using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L23_C01_asp_net_core_app.Data
{
    public interface ICitiesDataStore
    {
        public List<CityDto> Cities { get; }
    }
}
