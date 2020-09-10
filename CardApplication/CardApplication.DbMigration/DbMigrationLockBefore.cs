using System;
using FluentMigrator;

namespace CardApplication.DbMigration
{
    [Maintenance(MigrationStage.BeforeAll, TransactionBehavior.None)]
    public class DbMigrationLockBefore : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException("Down migrations are not supported for sp_getapplock");
        }

        public override void Up()
        {
            Execute.Sql(@"
            DECLARE @result INT
            EXEC @result = sp_getapplock 'CardApplication', 'Exclusive', 'Session'

            IF @result < 0
            BEGIN
                DECLARE @msg NVARCHAR(1000) = 'Received error code ' + CAST(@result AS VARCHAR(10)) + ' from sp_getapplock during migrations';
                THROW 99999, @msg, 1;
            END
        ");
        }
    }
}