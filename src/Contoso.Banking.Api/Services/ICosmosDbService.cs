using Contoso.Banking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Banking.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Transaction>> GetMultipleTransactionsAsync(string query);
        Task<Transaction> GetTransactionByIdAsync(string id);
        Task AddTransactionAsync(Transaction item);
        Task UpdateTransactionAsync(string id, Transaction item);
        Task DeleteTransactionAsync(string id);
    }
}
