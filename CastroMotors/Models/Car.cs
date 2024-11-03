using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CastroMotors.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }  // Ano de fabricação

        [StringLength(50)]
        public string Color { get; set; }  // Cor do carro

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }  // Preço

        public string Description { get; set; }  // Descrição adicional do carro

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public int BrandId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        // Caminho da Imagem
        public string ImagePath { get; set; }

    }
}