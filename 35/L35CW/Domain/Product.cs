using System.ComponentModel.DataAnnotations.Schema;

namespace L35CW.Domain
{
	[Table("Product", Schema = "dbo")]
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}

}
