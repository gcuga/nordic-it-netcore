using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace L35CW.Domain
{
	[Table("OrderItem", Schema = "dbo")]
	public class OrderItem
	{
		public int Id { get; set; }

		//[Key, Column(Order = 1)]
		public int OrderId { get; set; }

		//[Key, Column(Order = 2)]
		public int ProductId { get; set; }

		public int NumberOfItems { get; set; }

		[ForeignKey("OrderId")]
		public Order order { get; set; }
	}

}
