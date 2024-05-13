using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static  void AddInfrastructure(this IServiceCollection services)
    {
        AddDbContext(services);
        AddRepositories(services);
    }

    private static void AddDbContext(IServiceCollection services)
    {
        var connectionString = "";
        services.AddDbContext<MyRecipeBookDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRopository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRopository>();
    }
}
