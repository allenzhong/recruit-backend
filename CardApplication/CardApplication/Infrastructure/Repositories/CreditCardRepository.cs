using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public async Task Create(CreditCard card)
        {
            try
            {
                const string sql = @"
                INSERT INTO [dbo].[CreditCards] (
                        [Name],
                        [CardNumber],
                        [EncryptedCvc],
                        [ExpiryDate],
                        [CvcSalt]
                        )
                VALUES (
                        @Name,
                        @CardNumber,
                        @EncryptedCvc,
                        @ExpiryDate,
                        @CvcSalt
                )
            ";

                await _connection.ExecuteAsync(sql, new
                {
                    card.Name,
                    card.CardNumber,
                    card.EncryptedCvc,
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

        public async Task<IEnumerable<CreditCard>> Get()
        {
            const string sql = @"
                SELECT  [Id],
                        [Name],
                        [CardNumber],
                        [EncryptedCvc],
                        [ExpiryDate],
                        [CvcSalt] 
                FROM [dbo].[CreditCards] 
            ";

            return await _connection.QueryAsync<CreditCard>(sql);
        }

        public async Task<CreditCard> GetById(long id)
        {
            const string sql = @"
                SELECT  [Id],
                        [Name],
                        [CardNumber],
                        [EncryptedCvc],
                        [ExpiryDate],
                        [CvcSalt]  
                FROM [dbo].[CreditCards] 
                WHERE Id = @Id
            ";
            return await _connection.QueryFirstAsync<CreditCard>(sql, new
                {Id = id});
        }
    }
}