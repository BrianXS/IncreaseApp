using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IncreaseApp.Enums;

namespace IncreaseApp.Entities
{
    public class TransactionDiscount
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public DiscountType DiscountType { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}