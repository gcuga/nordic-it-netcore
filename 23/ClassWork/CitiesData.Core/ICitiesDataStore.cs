using System.Collections.Generic;

namespace CitiesData.Core
{
    public interface ICitiesDataStore
    {
        List<CityDto> Cities { get; }
    }
}
