using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace L35CW.Domain
{
	[Table("Order", Schema = "dbo")]
	public class Order
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public decimal Discount { get; set; }
		//List<OrderItem> OrderItems { get; set; }
	}

}
