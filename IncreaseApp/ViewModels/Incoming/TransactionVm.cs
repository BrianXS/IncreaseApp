using System.Collections.Generic;

namespace IncreaseApp.ViewModels.Incoming
{
    public class TransactionVm
    {
        public TransactionVm()
        {
            Details = new List<TransactionDetailVm>();
            Discounts = new List<TransactionDiscountVm>();
        }
        
        public HeaderVm Header { get; set; }
        public List<TransactionDetailVm> Details { get; set; }
        public List<TransactionDiscountVm> Discounts { get; set; }
        public FooterVm Footer { get; set; }
    }
}