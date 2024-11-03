using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CastroMotors.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }  // Descrição da categoria

        [StringLength(20)]
        public string Code { get; set; }  // Código único da categoria para fins administrativos

        public List<Car> Cars { get; set; }
    }
}