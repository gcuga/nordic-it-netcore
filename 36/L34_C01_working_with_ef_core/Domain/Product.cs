using System.ComponentModel.DataAnnotations.Schema;

namespace L34_C01_working_with_ef_core.Domain
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }
	}
}