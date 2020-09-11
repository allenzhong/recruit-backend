using FluentMigrator;

namespace CardApplication.DbMigration
{
    [Migration(202009111730)]
    public class _202009111730_RenameCvcToEncryptedCvcColumn: Migration
    {        
        private const string TableName = "CreditCards";
        private const string CvcColumnName = "Cvc";
        private const string EncryptedCvcColumnName = "EncryptedCvc";

        public override void Up()
        {
            Rename.Column(CvcColumnName).OnTable(TableName).To(EncryptedCvcColumnName);
        }

        public override void Down()
        {
            Rename.Column(EncryptedCvcColumnName).OnTable(TableName).To(CvcColumnName);
        }
    }
}