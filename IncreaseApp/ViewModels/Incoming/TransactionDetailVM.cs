using System;
using IncreaseApp.Enums;

namespace IncreaseApp.ViewModels.Incoming
{
    public class TransactionDetailVM
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Status Status { get; set; }

        public static TransactionDetailVM StringToTransactionDetailVM(string plainTransactionDetailVm)
        {
            return new TransactionDetailVM
            {
                Id = Guid.Parse(plainTransactionDetailVm.Substring(1, 32)),
                Amount = decimal.Parse(plainTransactionDetailVm.Substring(33, 13)),
                Status = (Status) int.Parse(plainTransactionDetailVm.Substring(51,1))
            };
        }
    }
}