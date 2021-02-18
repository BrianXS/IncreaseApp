using System;
using IncreaseApp.Enums;

namespace IncreaseApp.ViewModels.Incoming
{
    public class TransactionDiscountVm
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DiscountType DiscountType { get; set; }

        public static TransactionDiscountVm StringToDiscountVm(string plainTextDiscountVm)
        {
            return new TransactionDiscountVm
            {
                Id = Guid.Parse(plainTextDiscountVm.Substring(1, 32)),
                Amount = decimal.Parse(plainTextDiscountVm.Substring(33, 13)),
                DiscountType = (DiscountType) int.Parse(plainTextDiscountVm.Substring(49, 1))
            };
        }
    }
}