using System;
using IncreaseApp.Enums;

namespace IncreaseApp.ViewModels.Incoming
{
    public class DiscountVM
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DiscountType DiscountType { get; set; }

        public static DiscountVM StringToDiscountVM(string plainTextDiscountVM)
        {
            return new DiscountVM
            {
                Id = Guid.Parse(plainTextDiscountVM.Substring(1, 32)),
                Amount = decimal.Parse(plainTextDiscountVM.Substring(33, 13)),
                DiscountType = (DiscountType) int.Parse(plainTextDiscountVM.Substring(49, 1))
            };
        }
    }
}