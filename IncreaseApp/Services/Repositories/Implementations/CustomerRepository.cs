using System;
using System.Linq;
using AutoMapper;
using IncreaseApp.Entities;
using IncreaseApp.Services.Database;
using IncreaseApp.Services.Repositories.Interfaces;
using IncreaseApp.ViewModels;
using IncreaseApp.ViewModels.Outgoing;
using Microsoft.EntityFrameworkCore;

namespace IncreaseApp.Services.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IncreaseDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerRepository(IncreaseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public CustomerDataVM FindCustomerById(Guid id)
        {
            return _mapper.Map<CustomerDataVM>
                (_dbContext.Customers
                    .Include(x => x.Transactions)
                    .Include(x => x.Discounts)
                    .FirstOrDefault(x => x.Id.Equals(id)));
        }

        public void CreateRandomCustomer()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Juan",
                LastName = "Perez",
                Address = "Calle falsa 123",
                Country = "Polombia",
                Email = "juanperez@gmai.com",
                Phone = "312-123-1232",
                Job = "Programmer"
            };

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}