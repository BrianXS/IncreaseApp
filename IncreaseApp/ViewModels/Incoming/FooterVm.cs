using System;
using System.Globalization;

namespace IncreaseApp.ViewModels.Incoming
{
    public class FooterVm
    {
        public DateTime PaymentDate { get; set; }
        public Guid CustomerId { get; set; }

        public static FooterVm StringToFooterVm(string plainTextFooterVm)
        {
            return new FooterVm
            {
                PaymentDate = DateTime.ParseExact(plainTextFooterVm.Substring(16, 8), "yyyyMMdd", CultureInfo.InvariantCulture),
                CustomerId = Guid.Parse(plainTextFooterVm.Substring(24, 32))
            };
        }
    }
}