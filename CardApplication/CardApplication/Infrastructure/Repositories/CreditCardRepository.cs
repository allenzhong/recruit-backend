using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CardApplication.Domain.Models;
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
            const string sql = @"
                INSERT INTO [dbo].[CreditCards] (
                        [Name],
                        [CardNumber],
                        [Cvc],
                        [ExpiryDate]
                        )
                VALUES (
                        @Name,
                        @CardNumber,
                        @Cvc,
                        @ExpiryDate
                )
            ";

            await _connection.ExecuteAsync(sql, new
            {
                card.Name,
                card.CardNumber,
                card.Cvc,
                card.ExpiryDate
            });
        }
    }
}