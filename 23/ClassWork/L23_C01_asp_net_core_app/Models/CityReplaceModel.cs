using CitiesData.Core;

namespace L23_C01_asp_net_core_app.Models
{
	public class CityReplaceModel : CityCommonValidationModel
	{
		public CityReplaceModel()
		{
		}

		public CityReplaceModel(CityDto city)
		{
			Name = city.Name;
			Description = city.Description;
		}
	}
}
