using System.Diagnostics.CodeAnalysis;
using FluentMigrator;

namespace CardApplication.DbMigration
{
    [ExcludeFromCodeCoverage]
    [Migration(202009110120)]
    // ReSharper disable once InconsistentNaming
    public class _202009110120_AddCvcSaltColumn : Migration
    {
        private const string TableName = "CreditCards";
        private const string ColumnName = "CvcSalt";
        
        public override void Up()
        {
            Alter.Table(TableName)
                .AddColumn(ColumnName).AsString(10).NotNullable().WithDefaultValue("");

        }

        public override void Down()
        {
            Delete.Column(ColumnName).FromTable(TableName);
        }
    }
    
    
}