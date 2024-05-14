using Dapper;
using Npgsql;

namespace MyRecipeBook.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static void Migrate(string connectionString)
    {
        EnsureDatabaseCreated_Postgree(connectionString);
    }

    private static void EnsureDatabaseCreated_Postgree(string connectionString)
    {
        var connectStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);

        var databaseName = connectStringBuilder.Database;

        connectStringBuilder.Remove("Database");

        using var dbConnection = new NpgsqlConnection(connectStringBuilder.ConnectionString);


        var records = dbConnection.Query("select datname from pg_database where datname = @databasename", new { databasename  = databaseName});

        if(records.Any() == false)
            dbConnection.Execute($"CREATE DATABASE {databaseName}");
    }
}
