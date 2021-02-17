using System.Collections.Generic;

namespace IncreaseApp.ViewModels.Incoming
{
    public class FileVM
    {
        public FileVM()
        {
            Transactions = new List<TransactionVM>();
        }
        public List<TransactionVM> Transactions { get; set; }
        
    }
}