using Contoso.Banking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace Contoso.Banking.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;
        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddTransactionAsync(Transaction item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }
        public async Task DeleteTransactionAsync(string id)
        {
            await _container.DeleteItemAsync<Transaction>(id, new PartitionKey(id));
        }
        public async Task<Transaction> GetTransactionByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Transaction>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<Transaction>> GetMultipleTransactionsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Transaction>(new QueryDefinition(queryString));
            var results = new List<Transaction>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateTransactionAsync(string id, Transaction item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }

        // Create a method which takes in a Users object and adds it to the database
        
        // Create a method which takes in a Users object and updates it in the database

        // Create a method which takes in a Users object and deletes it from the database

        // Create a method which takes a string id and returns a Users object from the database

        // Create a method which takes a string query and returns a list of Users objects from the database

    }
}
