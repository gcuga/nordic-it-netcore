using System;
using System.Collections.Generic;

namespace L34_C02_working_with_ef_core_final.Domain
{
	public class Order
	{
		public Order()
		{
			OrderItems = new List<OrderItem>();
		}

		public int Id { get; set; }

		public Customer Customer { get; set; }

		public DateTimeOffset OrderDate { get; set; }

		public decimal Discount { get; set; }

		public List<OrderItem> OrderItems { get; set; }
	}
}
