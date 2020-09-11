using System.Diagnostics.CodeAnalysis;
using FluentMigrator;

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
                .WithStringNotNullableColumn("Cvc", 10)
                .WithColumn("ExpiryDate").AsDate().NotNullable();
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
    
    
}