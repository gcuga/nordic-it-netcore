using System.Collections.Generic;

namespace L23_C01_asp_net_core_app.Data
{
	public class CitiesDataStore : ICitiesDataStore
	{
		//private static CitiesDataStore _store;

		public List<CityDto> Cities { get; }

		public CitiesDataStore()
		{
			Cities = new List<CityDto>
			{
				new CityDto(1, "Moscow", "The capital of our Motherland"),
				new CityDto(2, "Saint-Petersburg", "the city I am originally from"),
				new CityDto(3, "New-York", "The city I would like to take a look at :)")
			};
		}

		//public static CitiesDataStore GetInstance()
		//{
		//	if (_store == null)
		//		_store = new CitiesDataStore();

		//	return _store;
		//}
	}
}
