using L23_C01_asp_net_core_app.Data;

namespace L23_C01_asp_net_core_app.Models
{
	public class CityReplaceModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

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
