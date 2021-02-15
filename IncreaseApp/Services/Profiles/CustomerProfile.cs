using System;
using System.Linq;
using AutoMapper;
using IncreaseApp.Entities;
using IncreaseApp.ViewModels;
using IncreaseApp.ViewModels.Outgoing;

namespace IncreaseApp.Services.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDataVM>()
                .ForMember(to => to.MoneyThatWasCharged,
                    from => from
                        .MapFrom(src => (src.Transactions
                                             .Where(x => DateTime.Compare(DateTime.Now, x.PaymentDate) >= 0)
                                             .Sum(x => x.Amount) - 
                                        src.Discounts
                                            .Where(x => DateTime.Compare(DateTime.Now, x.DiscountDate) >= 0)
                                            .Sum(x => x.Amount)).ToString("C0")))
                .ForMember(to => to.MoneyThatWasCharged,
                    from => from
                        .MapFrom(src => (src.Transactions
                                             .Where(x => DateTime.Compare(DateTime.Now, x.PaymentDate) < 0)
                                             .Sum(x => x.Amount) - 
                                         src.Discounts
                                             .Where(x => DateTime.Compare(DateTime.Now, x.DiscountDate) < 0)
                                             .Sum(x => x.Amount)).ToString("C0")));
        }
    }
}