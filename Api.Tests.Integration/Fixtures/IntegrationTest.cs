using System.Data.Common;
using Infrastructure.Data;
using Infrastructure.SeedData;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Api.Tests.Integration.Fixtures;

[Trait("Category", "Integration")]
public abstract class IntegrationTest 
{
    protected readonly HttpClient _client;
    protected readonly WebApplicationFactory<Program> _factory;

    protected IntegrationTest()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                // Create open SqliteConnection so EF won't automatically close it.
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<ApplicationDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
            });
        });
        // using (var scope = _factory.Services.CreateScope())
        // {
        //     var scopedServices = scope.ServiceProvider;
        //     var context = scopedServices.GetRequiredService<ApplicationDbContext>();
        //     var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();
        //
        //     try
        //     {
        //         context.Database.MigrateAsync();
        //         StoreContextSeed.SeedAsync(context, loggerFactory);
        //     }
        //     catch (Exception ex)
        //     {
        //         var logger = loggerFactory.CreateLogger<Program>();
        //         logger.LogError(ex, "An error occured during migration");
        //     }
        // }
        
        _client = _factory.CreateClient();
    }
}