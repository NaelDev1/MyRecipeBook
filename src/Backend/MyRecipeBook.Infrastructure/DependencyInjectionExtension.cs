using FluentMigrator.Runner;
using FluentMigrator.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using System.Reflection;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static  void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);

        bool isTesting = configuration.GetValue<bool>("InMemoryTest");
        if (isTesting)
            return;

        AddDbContext(services, configuration);
        AddFluentMigrator_Postgree(services, configuration);


    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {


        var connectionString = configuration.GetConnectionString("ConnectionPostgree");
        services.AddDbContext<MyRecipeBookDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRopository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRopository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddFluentMigrator_Postgree(IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetConnectionString("ConnectionPostgree");
        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options.WithGlobalConnectionString(connectionStrings)
            .AddPostgres()
            .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
        });
    }

}
