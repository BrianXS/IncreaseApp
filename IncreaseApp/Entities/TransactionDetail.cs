using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IncreaseApp.Enums;

namespace IncreaseApp.Entities
{
    public class TransactionDetail
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public Status Status { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}