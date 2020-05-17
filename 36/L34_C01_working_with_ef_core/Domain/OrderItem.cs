using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L34_C01_working_with_ef_core.Domain
{
	[Table("OrderItem", Schema = "dbo")]
	public class OrderItem
	{
		public int OrderId { get; set; }

		public int ProductId { get; set; }

		public int NumberOfItems { get; set; }
	}

}
