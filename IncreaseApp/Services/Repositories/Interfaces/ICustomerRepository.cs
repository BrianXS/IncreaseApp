using System;
using System.Threading.Tasks;
using IncreaseApp.Entities;
using IncreaseApp.ViewModels;
using IncreaseApp.ViewModels.Outgoing;

namespace IncreaseApp.Services.Repositories.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {
        CustomerDataVm FindCustomerById(Guid id);
        bool DoesCustomerExist(Guid id);
        Task SearchAndCreateUser(Guid id);
    }
}