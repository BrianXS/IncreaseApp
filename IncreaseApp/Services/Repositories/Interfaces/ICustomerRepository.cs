using System;
using IncreaseApp.Entities;
using IncreaseApp.ViewModels;
using IncreaseApp.ViewModels.Outgoing;

namespace IncreaseApp.Services.Repositories.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {
        CustomerDataVM FindCustomerById(Guid id);
        void CreateRandomCustomer();
    }
}