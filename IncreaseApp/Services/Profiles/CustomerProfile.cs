using System;
using System.Linq;
using AutoMapper;
using IncreaseApp.Entities;
using IncreaseApp.ViewModels;
using IncreaseApp.ViewModels.Incoming;
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
                        .MapFrom(src => src.Transactions.Sum(x => x.TotalAmountWithDiscounts).ToString("C0")))
                .ForMember(to => to.MoneyThatWasCharged,
                    from => from
                        .MapFrom(src => src.Transactions.Sum(x => x.TotalAmountWithDiscounts).ToString("C0")));

            CreateMap<CustomerVM, Customer>()
                .ForMember(to => to.Id, 
                    from => from.MapFrom(src => Guid.Parse(src.Id)));
        }
    }
}