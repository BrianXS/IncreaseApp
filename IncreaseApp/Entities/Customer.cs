using System;
using System.Collections.Generic;

namespace IncreaseApp.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        
        public string Email { get; set; }
        public string Job { get; set; }
        
        public string Country { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}