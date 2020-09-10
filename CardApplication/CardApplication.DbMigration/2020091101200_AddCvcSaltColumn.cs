using System.Diagnostics.CodeAnalysis;
using FluentMigrator;

namespace CardApplication.DbMigration
{
    [ExcludeFromCodeCoverage]
    [Migration(2020091101200)]
    // ReSharper disable once InconsistentNaming
    public class _2020091101200_AddCvcSaltColumn : Migration
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