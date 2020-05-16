using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CorrespondenceEF.Domain
{
    [Table("City", Schema = "dbo")]
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
    }
}
