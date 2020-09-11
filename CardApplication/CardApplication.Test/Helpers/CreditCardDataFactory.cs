using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CardApplication.Domain.Models;
using Dapper;

namespace CardApplication.Test.Helpers
{
    public class CreditCardDataFactory
    {
        public static async Task<IEnumerable<CreditCard>> CreateCreditCards(IDbConnection connection, int numberOfCreditCards)
        {
            var creditCards = CreditCardGenerator.CreateValidCreditCardFaker().Generate(numberOfCreditCards);
            foreach (var c in creditCards)
            {
                await connection.ExecuteAsync(@"
                INSERT INTO [dbo].[CreditCards] (
                        [Name],
                        [CardNumber],
                        [EncryptedCvc],
                        [CvcSalt],
                        [ExpiryDate]
                        )
                VALUES (
                        @Name,
                        @CardNumber,
                        @EncryptedCvc,
                        @CvcSalt,
                        @ExpiryDate
                )                
                ", new
                {
                    c.Name,
                    c.CardNumber,
                    c.EncryptedCvc,
                    c.CvcSalt,
                    c.ExpiryDate
                });
            }


            var queryAll = @"SELECT * FROM CreditCards";
            var result = await connection.QueryAsync<CreditCard>(queryAll);
            
            return result;
        }
    }
}