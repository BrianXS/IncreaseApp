using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using IncreaseApp.Entities;
using IncreaseApp.Services.Database;
using IncreaseApp.Services.Repositories.Interfaces;
using IncreaseApp.ViewModels;
using IncreaseApp.ViewModels.Incoming;
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

        public async Task SearchAndCreateUser(Guid id)
        {
            if (!DoesCustomerExist(id))
            {
                var httpClient = _httpClientFactory.CreateClient("IncreaseAPI");
                string plainUser = null;

                while (plainUser == null)
                {
                    try
                    {
                        plainUser = await httpClient.GetStringAsync($"/clients/{id:N}");
                    } finally{ }
                }
                
                var customer = JsonSerializer.Deserialize<CustomerVM>(plainUser.Replace("-", ""), new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                
                await _dbContext.Customers.AddAsync(_mapper.Map<Customer>(customer));
                await _dbContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}