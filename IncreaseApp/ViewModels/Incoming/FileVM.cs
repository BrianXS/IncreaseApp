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
    
    //TODO: Map Each row (properly with data typechecks to avoid failures)
    //TODO: Create rules for storing each row on the bd 
    //TODO: Verify whether the customer already exists
    //TODO: Verify whether the transaction and any of its sub entities already exists
}