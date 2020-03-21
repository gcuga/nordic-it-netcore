using L23_C01_asp_net_core_app.Data;

namespace L23_C01_asp_net_core_app.Models
{
	public class CityGetModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public CityGetModel()
		{
		}

		public CityGetModel(CityDto city)
		{
			Id = city.Id;
			Name = city.Name;
			Description = city.Description;
		}
	}
}
