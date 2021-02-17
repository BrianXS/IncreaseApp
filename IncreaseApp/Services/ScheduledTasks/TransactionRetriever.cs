using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IncreaseApp.Services.Repositories.Interfaces;
using IncreaseApp.Util;
using IncreaseApp.ViewModels.Incoming;
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
                FileVM fileVm = null;

                while (fileVm == null)
                {
                    try
                    {
                        fileVm = TransactionUtility
                            .TransactionBatchSplitter(await httpClient.GetStringAsync("/file.txt"));
                    } finally{ }
                }
                
                await FileToBD(fileVm);
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        protected async Task FileToBD(FileVM fileVm)
        {
            await SaveUsersThatDontExist(fileVm.Transactions.Select(x => x.Footer.CustomerId).ToList());
            //Todo: Store Header Details and Discounts
        }
        
        protected async Task SaveUsersThatDontExist(List<Guid> userIds)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                foreach (var userId in userIds)
                {
                    await repository.SearchAndCreateUser(userId);
                }
            }
        }
    }
}