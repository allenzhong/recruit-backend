using System;
using FluentMigrator;

namespace CardApplication.DbMigration
{
    [Maintenance(MigrationStage.AfterAll, TransactionBehavior.None)]
    public class DbMigrationUnlockAfter : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException("Down migrations are not supported for sp_releaseapplock");
        }

        public override void Up()
        {
            Execute.Sql("EXEC sp_releaseapplock 'CardApplication', 'Session'");
        }
    }
}