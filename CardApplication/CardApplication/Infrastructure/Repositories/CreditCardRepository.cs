using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using CardApplication.Domain.Models;
using CardApplication.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CardApplication.Infrastructure.Repositories
{
    public class CreditCardRepository: ICreditCardRepository
    {
        private IDbConnection _connection;
        private ILogger<CreditCardRepository> _logger;

        public CreditCardRepository(IDbConnection connection, ILogger<CreditCardRepository> logger)
        {
            _logger = logger;
            _connection = connection;
        }
        public async Task Create(CreditCard card, CancellationToken cancellationToken)
        {
            try
            {
                const string sql = @"
                INSERT INTO [dbo].[CreditCards] (
                        [Name],
                        [CardNumber],
                        [Cvc],
                        [ExpiryDate],
                        [CvcSalt]
                        )
                VALUES (
                        @Name,
                        @CardNumber,
                        @Cvc,
                        @ExpiryDate,
                        @CvcSalt
                )
            ";

                await _connection.ExecuteAsync(sql, new
                {
                    card.Name,
                    card.CardNumber,
                    card.Cvc,
                    card.ExpiryDate,
                    card.CvcSalt
                });
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("UNIQUE KEY constraint 'UC_Name_CardNumber'"))
                {
                    throw new CreditCardRecordExistingException();
                }

                throw;
            }
        }
    }
}