using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorrespondenceEF.Domain
{
    [Table("Office", Schema = "dbo")]
    public class Office
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CityId { get; set; }
        
        [ForeignKey("CityId")]
        public City city { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string Address { get; set; }
    }
}
