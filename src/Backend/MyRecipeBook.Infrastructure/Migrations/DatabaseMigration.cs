using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace MyRecipeBook.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static void Migrate(string connectionString, IServiceProvider provider)
    {
        EnsureDatabaseCreated_Postgree(connectionString);
        MigrationDatabase(provider);
    }

    private static void EnsureDatabaseCreated_Postgree(string connectionString)
    {
        var connectStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);

        var databaseName = connectStringBuilder.Database;

        connectStringBuilder.Remove("Database");

        using var dbConnection = new NpgsqlConnection(connectStringBuilder.ConnectionString);


        var records = dbConnection.Query("select datname from pg_database where datname = @databasename", new { databasename  = databaseName});

        if(!records.Any())
            dbConnection.Execute($"CREATE DATABASE {databaseName}");
    }


    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();

    }
}
