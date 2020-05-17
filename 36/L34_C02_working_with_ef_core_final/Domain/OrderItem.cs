using System.ComponentModel.DataAnnotations;

namespace L34_C02_working_with_ef_core_final.Domain
{
	public class OrderItem
	{
		public int OrderId { get; set; }

		public Product Product { get; set; }

		[Range(1, 100)]
		public int NumberOfItems { get; set; }
	}
}
