using System;
using IncreaseApp.Enums;

namespace IncreaseApp.ViewModels.Incoming
{
    public class HeaderVm
    {
        public Guid Id { get; set; }
        public Currency Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalWithDiscounts { get; set; }
        public decimal TotalAmountWithDiscounts { get; set; }

        public static HeaderVm StringToHeaderVm(string plainHeaderVm)
        {
            return new HeaderVm
            {
                Id = Guid.Parse(plainHeaderVm.Substring(1, 32)),
                Currency = (Currency) int.Parse(plainHeaderVm.Substring(36, 3)),
                TotalAmount = decimal.Parse(plainHeaderVm.Substring(39, 13)),
                TotalWithDiscounts = decimal.Parse(plainHeaderVm.Substring(52, 13)),
                TotalAmountWithDiscounts = decimal.Parse(plainHeaderVm.Substring(65, 13))
            };
        }
    }
}