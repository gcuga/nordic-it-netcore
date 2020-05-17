using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L34_C01_working_with_ef_core.Domain
{
	[Table("Customer", Schema = "dbo")]
	public class Customer
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[Column("Name", TypeName = "VARCHAR(50)")]

		public string Name { get; set; }
	}
}