using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public CustomerRepository(IncreaseDbContext dbContext,
            IHttpClientFactory httpClientFactory,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }
        
        public CustomerDataVM FindCustomerById(Guid id)
        {
            return _mapper.Map<CustomerDataVM>
                (_dbContext.Customers
                .Include(x => x.Transactions).ThenInclude(x => x.Details)
                .Include(x => x.Transactions).ThenInclude(x => x.Discounts)
                .FirstOrDefault(x => x.Id.Equals(id)));
        }

        public bool DoesCustomerExist(Guid id)
        {
            return _dbContext.Customers.Any(x => x.Id == id);
        }

        public async void SearchAndCreateUser(Guid id)
        {
            if (!DoesCustomerExist(id))
            {
                var httpClient = _httpClientFactory.CreateClient("IncreaseAPI");
                var success = false;
                var plainUser = "";

                while (!success)
                {
                    try
                    {
                        plainUser = await httpClient.GetStringAsync($"/clients/{id:N}");
                        if (!string.IsNullOrEmpty(plainUser.Trim())) success = true;
                    }
                    catch (Exception e) { }
                }
                
                var customer = JsonSerializer.Deserialize<Customer>(plainUser, new JsonSerializerOptions());
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}