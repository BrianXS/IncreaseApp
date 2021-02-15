using System;
using System.Threading;
using System.Threading.Tasks;
using IncreaseApp.Services.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IncreaseApp.Services.ScheduledTasks
{
    public class TransactionRetriever : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceProvider;

        public TransactionRetriever(IServiceScopeFactory serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                    repository.CreateRandomCustomer();
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }
    }
}