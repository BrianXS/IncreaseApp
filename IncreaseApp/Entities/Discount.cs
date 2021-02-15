using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IncreaseApp.Enums;

namespace IncreaseApp.Entities
{
    public class Discount
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public DateTime DiscountDate { get; set; }
        public DiscountType DiscountType { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}