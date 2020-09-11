using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CardApplication.Domain.Models;

namespace CardApplication.Infrastructure.Repositories
{
    public interface ICreditCardRepository
    {
        Task Create(CreditCard card);
        Task<IEnumerable<CreditCard>> Get();
        Task<CreditCard> GetById(long id);
    }
}