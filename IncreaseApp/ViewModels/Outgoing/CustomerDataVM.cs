using System;
using System.Collections.Generic;
using IncreaseApp.Entities;

namespace IncreaseApp.ViewModels.Outgoing
{
    public class CustomerDataVM
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }
        public string Job { get; set; }
        
        public string Country { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }

        public string MoneyToBeCharged { get; set; }
        public string MoneyThatWasCharged { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}