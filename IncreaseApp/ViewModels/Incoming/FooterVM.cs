using System;
using System.Globalization;

namespace IncreaseApp.ViewModels.Incoming
{
    public class FooterVM
    {
        public DateTime PaymentDate { get; set; }
        public Guid CustomerId { get; set; }

        public static FooterVM StringToFooterVM(string plainTextFooterVM)
        {
            return new FooterVM
            {
                PaymentDate = DateTime.ParseExact(plainTextFooterVM.Substring(16, 8), "yyyyMMdd", CultureInfo.InvariantCulture),
                CustomerId = Guid.Parse(plainTextFooterVM.Substring(24, 32))
            };
        }
    }
}