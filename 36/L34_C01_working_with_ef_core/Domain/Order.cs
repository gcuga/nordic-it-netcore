using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L34_C01_working_with_ef_core.Domain
{
	[Table("Order", Schema = "dbo")]
	public class Order
	{
		[Key]
		public int Id { get; set; }

		public int CustomerId { get; set; }

		public DateTimeOffset OrderDate { get; set; }

		public decimal Discount { get; set; }

		public List<OrderItem> OrderItems { get; set; }
	}
}