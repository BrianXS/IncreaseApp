using System;
using IncreaseApp.Entities;
using IncreaseApp.ViewModels;
using IncreaseApp.ViewModels.Outgoing;

namespace IncreaseApp.Services.Repositories.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {
        CustomerDataVM FindCustomerById(Guid id);
        bool DoesCustomerExist(Guid id);
        void SearchAndCreateUser(Guid id);
    }
}