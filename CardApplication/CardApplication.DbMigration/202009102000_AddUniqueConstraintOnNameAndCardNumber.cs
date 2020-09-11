using System.Diagnostics.CodeAnalysis;
using FluentMigrator;

namespace CardApplication.DbMigration
{
    [ExcludeFromCodeCoverage]
    [Migration(202009102000)]
    // ReSharper disable once InconsistentNaming
    public class _202009102000_AddUniqueConstraintOnNameAndCardNumber : Migration
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