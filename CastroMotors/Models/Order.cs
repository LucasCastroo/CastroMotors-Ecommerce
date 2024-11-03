using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CastroMotors.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string UserId { get; set; }

        public bool IsFinalized { get; set; }  // Indica se o pedido foi finalizado

        public List<OrderItem> OrderItems { get; set; }

    }
}