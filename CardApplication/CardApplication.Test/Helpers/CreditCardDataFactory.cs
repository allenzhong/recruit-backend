using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CardApplication.Domain.Models;
using Dapper;

namespace CardApplication.Test.Helpers
{
    public class CreditCardDataFactory
    {
        public static async Task<List<CreditCard>> CreateCreditCards(IDbConnection connection, int numberOfCreditCards)
        {
            var creditCards = CreditCardGenerator.CreateValidCreditCardFaker().Generate(numberOfCreditCards);
            foreach (var c in creditCards)
            {
                await connection.ExecuteAsync(@"
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
                ", new
                {
                    c.Name,
                    c.CardNumber,
                    c.Cvc,
                    c.ExpiryDate
                });
            }

            return creditCards;
        }
    }
}