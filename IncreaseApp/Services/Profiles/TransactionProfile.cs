using AutoMapper;
using IncreaseApp.Entities;
using IncreaseApp.ViewModels.Incoming;

namespace IncreaseApp.Services.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<HeaderVm, Transaction>();
            CreateMap<TransactionDetailVm, TransactionDetail>();
            CreateMap<TransactionDiscountVm, TransactionDiscount>();
        }
    }
}