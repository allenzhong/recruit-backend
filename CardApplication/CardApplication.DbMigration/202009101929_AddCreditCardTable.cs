using System.Diagnostics.CodeAnalysis;
using FluentMigrator;
using FluentMigrator.Builders.Create.Sequence;
using FluentMigrator.Builders.Create.Table;

namespace CardApplication.DbMigration
{
    [ExcludeFromCodeCoverage]
    [Migration(202009101929)]
    // ReSharper disable once InconsistentNaming
    public class _202009101929_AddCreditCardTable : Migration
    {
        private const string TableName = "CreditCards";
        
        public override void Up()
        {
            Create.Table(TableName)
                .WithIdColumn()
                .WithStringNotNullableColumn("Name", 50)
                .WithStringNotNullableColumn("CardNumber", 20)
                .WithStringNotNullableColumn("Cvc", 10);
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
    
    
}