namespace CitiesData.Core
{
	public class CityDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public CityDto() { }

		public CityDto(int id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
