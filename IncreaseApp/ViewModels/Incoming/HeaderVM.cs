using System;
using IncreaseApp.Enums;

namespace IncreaseApp.ViewModels.Incoming
{
    public class HeaderVM
    {
        public Guid Id { get; set; }
        public Currency Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalWithDiscounts { get; set; }
        public decimal TotalAmountWithDiscounts { get; set; }

        public static HeaderVM StringToHeaderVm(string PlainHeaderVM)
        {
            return new HeaderVM
            {
                Id = Guid.Parse(PlainHeaderVM.Substring(1, 32)),
                Currency = (Currency) int.Parse(PlainHeaderVM.Substring(36, 3)),
                TotalAmount = decimal.Parse(PlainHeaderVM.Substring(39, 13)),
                TotalWithDiscounts = decimal.Parse(PlainHeaderVM.Substring(52, 13)),
                TotalAmountWithDiscounts = decimal.Parse(PlainHeaderVM.Substring(65, 13))
            };
        }
    }
}

//1 135615f850b04e40add09be3dcee0180   001 0000204692994 0000002807866 0000201885128
