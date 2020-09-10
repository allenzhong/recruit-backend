using System.Threading;
using System.Threading.Tasks;
using CardApplication.Domain.Models;

namespace CardApplication.Infrastructure.Repositories
{
    public interface ICreditCardRepository
    {
        Task Create(CreditCard card, CancellationToken cancellationToken);
    }
}