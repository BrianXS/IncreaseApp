using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IncreaseApp.Enums;

namespace IncreaseApp.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Currency Currency { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmount { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalDiscounts { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmountWithDiscounts { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public List<TransactionDetail> Details { get; set; }
        public List<Discount> Discounts { get; set; }
    }
}