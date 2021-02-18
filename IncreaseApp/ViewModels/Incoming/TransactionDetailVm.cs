using System;
using IncreaseApp.Enums;

namespace IncreaseApp.ViewModels.Incoming
{
    public class TransactionDetailVm
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Status Status { get; set; }

        public static TransactionDetailVm StringToTransactionDetailVm(string plainTransactionDetailVm)
        {
            return new TransactionDetailVm
            {
                Id = Guid.Parse(plainTransactionDetailVm.Substring(1, 32)),
                Amount = decimal.Parse(plainTransactionDetailVm.Substring(33, 13)),
                Status = (Status) int.Parse(plainTransactionDetailVm.Substring(51,1))
            };
        }
    }
}