using System.Collections.Generic;

namespace IncreaseApp.ViewModels.Incoming
{
    public class TransactionVM
    {
        public TransactionVM()
        {
            Details = new List<TransactionDetailVM>();
            Discounts = new List<DiscountVM>();
        }
        
        public HeaderVM Header { get; set; }
        public List<TransactionDetailVM> Details { get; set; }
        public List<DiscountVM> Discounts { get; set; }
        public FooterVM Footer { get; set; }
    }
}