using CitiesData.Core;

namespace L23_C01_asp_net_core_app.Models
{
	public class CityCreateModel : CityCommonValidationModel
	{
		public CityCreateModel()
		{
		}

		public CityCreateModel(CityDto city)
		{
			Name = city.Name;
			Description = city.Description;
		}

		public CityDto ToDto(int id)
		{
			var dto = new CityDto
			{
				Id = id,
				Name = Name,
				Description = Description
			};

			return dto;
		}
	}
}
