using System.Collections.Generic;

namespace IncreaseApp.ViewModels.Incoming
{
    public class FileVM
    {
        public List<TransactionVM> Transactions { get; set; }
        
    }

    //TODO: File Structure
    //TODO: Split and Map Each row
    //TODO: Create rules for storing each row on the bd 
    //TODO: I.E Verify whether the customer already exists, verify whether the  transaction already exists
}