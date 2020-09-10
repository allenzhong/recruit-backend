using System;

namespace CardApplication.DbMigration
{
    public class DatabaseMigrationException : Exception
    {
        public DatabaseMigrationException(Exception exception, string migrationFailed)
        {
            
        }
    }
}