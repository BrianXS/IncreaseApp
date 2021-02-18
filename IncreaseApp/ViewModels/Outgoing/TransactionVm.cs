using System;

namespace IncreaseApp.ViewModels.Outgoing
{
    public class TransactionVm
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscounts { get; set; }
        public decimal TotalAmountWithDiscounts { get; set; }
    }
}