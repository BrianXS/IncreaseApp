using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IncreaseApp.Services.Repositories.Interfaces;
using IncreaseApp.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IncreaseApp.Services.ScheduledTasks
{
    public class TransactionRetriever : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceProvider;
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionRetriever(IServiceScopeFactory serviceProvider, IHttpClientFactory httpClientFactory)
        {
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var httpClient = _httpClientFactory.CreateClient("IncreaseAPI");
                var results = TransactionUtility
                    .TransactionBatchSplitter(await httpClient.GetStringAsync("/file.txt"));
                
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}

/*
 * using (var scope = _serviceProvider.CreateScope())
    {
        var repository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
        repository.CreateRandomCustomer();
    }
*/