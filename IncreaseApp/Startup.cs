using System;
using IncreaseApp.Services.Database;
using IncreaseApp.Services.Repositories.Implementations;
using IncreaseApp.Services.Repositories.Interfaces;
using IncreaseApp.Services.ScheduledTasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace IncreaseApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IncreaseDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("local-brian"));
            });
            
            services.AddHttpClient("IncreaseAPI", client =>
            {
                client.BaseAddress = new Uri("https://increase-transactions.herokuapp.com/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer 1234567890qwertyuiopasdfghjklzxcvbnm");
                client.Timeout = TimeSpan.FromMinutes(1);
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddHostedService<TransactionRetriever>();
            services.AddControllers();
        }
        
        //Todo: Agregar metodos que mejoren la resiliencia del programa y eviten crasheos en input adverso
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}