using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Versions;


[Migration(DatabaseVersions.TABLE_USER,"Craeate table to save the user's information")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateColumn("Users")
            .WithColumn("Name").AsString(80).NotNullable()
            .WithColumn("Email").AsString(200).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable();

    }
}
