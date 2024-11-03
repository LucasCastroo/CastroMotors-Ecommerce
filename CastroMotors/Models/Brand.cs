using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CastroMotors.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string CountryOfOrigin { get; set; }  // País de origem da marca

        public int? FoundedYear { get; set; }  // Ano de fundação da marca (opcional)

        public List<Car> Cars { get; set; }
    }
}