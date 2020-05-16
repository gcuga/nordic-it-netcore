using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L35CW.Domain
{
	[Table("Customer", Schema = "dbo")]
	public class Customer
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
	}

}
