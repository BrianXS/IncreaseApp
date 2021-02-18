using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IncreaseApp.Entities;
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
                FileVm fileVm = null;

                while (fileVm == null)
                {
                    try
                    {
                        fileVm = TransactionUtility
                            .TransactionBatchSplitter(await httpClient.GetStringAsync("/file.txt"));
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
                
                await FileToBd(fileVm);
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task FileToBd(FileVm fileVm)
        {
            await SaveTransactionsAndUsers(fileVm.Transactions);
        }

        private async Task SaveTransactionsAndUsers(List<TransactionVm> transactions)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var usersRepository = scope.ServiceProvider
                    .GetRequiredService<ICustomerRepository>();
                
                var transactionsRepository = scope.ServiceProvider
                    .GetRequiredService<ITransactionRepository>();
                
                foreach (var transaction in transactions)
                {
                    await usersRepository.SearchAndCreateUser(transaction.Footer.CustomerId);
                    
                    transactionsRepository
                        .SaveTransaction(transaction.Header, 
                                         transaction.Footer.PaymentDate, 
                                         transaction.Footer.CustomerId);

                    transactionsRepository
                        .SaveAllDetails(transaction.Details, transaction.Header.Id);
                    
                    transactionsRepository
                        .SaveAllDiscounts(transaction.Discounts, transaction.Header.Id);
                }
            }
        }
    }
}