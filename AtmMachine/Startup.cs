using AtmMachine.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace AtmMachine
{
    public class Startup
    {
        //configuration handler
       private readonly IConfiguration _configuration;

        //cunstructor
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register the DbContext using SQLite
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(_configuration.GetConnectionString("Data Source=AtmDb.db;"));
            });

            // Register the account service and transaction service
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();

            // Register the controllers
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Other configuration code...

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}