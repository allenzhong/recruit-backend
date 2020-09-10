using System.Diagnostics.CodeAnalysis;
using FluentMigrator;

namespace CardApplication.DbMigration
{
    [ExcludeFromCodeCoverage]
    [Migration(202009110700)]
    // ReSharper disable once InconsistentNaming
    public class _202009110700_AddUniqueConstraintOnNameAndCardNumber : Migration
    {
        private const string TableName = "CreditCards";
        private const string UniqueConstraintName = "UC_Name_CardNumber";
        
        
        public override void Up()
        {
            Create.UniqueConstraint(UniqueConstraintName)
                .OnTable(TableName)
                .Columns("Name", "CardNumber");

        }

        public override void Down()
        {
            Delete.UniqueConstraint(UniqueConstraintName);
        }
    }
    
    
}