using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace MyRecipeBook.Infrastructure.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration
{

    protected ICreateTableColumnOptionOrWithColumnSyntax CreateColumn(string column)
    {
        return Create.Table(column)
            .WithColumn("Id").AsInt32().NotNullable().Identity()
            .WithColumn("Active").AsBoolean().WithDefaultValue(true)
            .WithColumn("CreateOn").AsDateTime().NotNullable();

    }

}
