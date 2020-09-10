using System.Threading;
using System.Threading.Tasks;
using CardApplication.Domain.Models;

namespace CardApplication.Infrastructure.Repositories
{
    public class CreditCardRepository: ICreditCardRepository
    {
        public Task Create(CreditCard card, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}