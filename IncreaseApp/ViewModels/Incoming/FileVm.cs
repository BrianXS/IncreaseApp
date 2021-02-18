using System.Collections.Generic;

namespace IncreaseApp.ViewModels.Incoming
{
    public class FileVm
    {
        public FileVm()
        {
            Transactions = new List<TransactionVm>();
        }
        public List<TransactionVm> Transactions { get; set; }
        
    }
}