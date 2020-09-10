using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Respawn;

namespace CardApplication.Test.Helpers
{
    public abstract class SqlDbTestBase
    {
        protected SqlConnection Connection;
        private static readonly Checkpoint Checkpoint = new Checkpoint()
        {
            TablesToIgnore = new [] {"VersionInfo"}
        };

        protected SqlDbTestBase()
        {
            SetupConnection();
        }

        protected void SetupConnection()
        {
            Connection = CreateConnection();
        }

        protected static SqlConnection CreateConnection()
        {
            var connectionString = TestHelper.GetConnectionString(Environment.CurrentDirectory);
            return new SqlConnection(connectionString);
        }

        protected async Task ResetDatabase()
        {
            var connectionString = TestHelper.GetConnectionString(Environment.CurrentDirectory);
            await Checkpoint.Reset(connectionString);
        }
    }
}
