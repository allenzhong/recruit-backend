using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace CardApplication.DbMigration
{
    public static class DbMigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnOrSchemaSyntax createTable)
        {
            return createTable.WithColumn("Id")
                .AsInt64()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }
        public static ICreateTableColumnOptionOrWithColumnSyntax WithStringNotNullableColumn(
            this ICreateTableColumnOptionOrWithColumnSyntax columnSyntax, string name, int size)
        {
            return columnSyntax.WithColumn(name)
                .AsString(size)
                .NotNullable();
        }
    }
}