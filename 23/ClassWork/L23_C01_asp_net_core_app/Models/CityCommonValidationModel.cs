using System.ComponentModel.DataAnnotations;
using L23_C01_asp_net_core_app.Validation;


namespace L23_C01_asp_net_core_app.Models
{
    public abstract class CityCommonValidationModel
    {
		[Required]
		[MinLength(2)]
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(300)]
		[DifferentValue("Name")]
		public string Description { get; set; }
	}
}
