using FluentMigrator;

namespace CardApplication.DbMigration
{
    [Migration(202009110130)]
    public class _202009110130_AlterCvcAndCvcSaltColumnLength: Migration
    {        
        private const string TableName = "CreditCards";
        private const string CvcColumnName = "Cvc";
        private const string CvcSaltColumnName = "CvcSalt";

        public override void Up()
        {
            Alter.Column(CvcColumnName)
                .OnTable(TableName)
                .AsString(50)
                .NotNullable()
                .WithDefaultValue("");
            Alter.Column(CvcSaltColumnName)
                .OnTable(TableName)
                .AsString(50)
                .NotNullable()
                .WithDefaultValue("");
        }

        public override void Down()
        {
            Alter.Column(CvcColumnName).OnTable(TableName).AsString(10);
            Alter.Column(CvcSaltColumnName).OnTable(TableName).AsString(10);
        }
    }
}