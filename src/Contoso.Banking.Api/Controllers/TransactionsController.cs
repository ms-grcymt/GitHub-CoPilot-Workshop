using Contoso.Banking.Models;
using Contoso.Banking.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Banking.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        public TransactionsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }


        // GET /Transactions
        [HttpGet]
        [ProducesResponseType(typeof(List<Transaction>), 200)]
        public async Task<IActionResult> List()
        {
            return Ok(await _cosmosDbService.GetMultipleTransactionsAsync("SELECT * FROM c"));
        }

        // GET /Transactions/23dbf68d-3f40-41de-ae1b-8e3558cd17f9
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transaction), 200)]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _cosmosDbService.GetTransactionByIdAsync(id));
        }

        // POST /Transactions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transaction item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddTransactionAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT /Transactions/23dbf68d-3f40-41de-ae1b-8e3558cd17f9
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] Transaction item)
        {
            await _cosmosDbService.UpdateTransactionAsync(item.Id, item);
            return NoContent();
        }

        // DELETE /Transactions/23dbf68d-3f40-41de-ae1b-8e3558cd17f9
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cosmosDbService.DeleteTransactionAsync(id);
            return NoContent();
        }

        // Create a method which takes a userid and returns all transactions for that user
        // GET /Transactions/ByUser/1
        [HttpGet("ByUser/{userid}")]
        [ProducesResponseType(typeof(List<Transaction>), 200)]
        public async Task<IActionResult> GetByUser(int userid)
        {
            return Ok(await _cosmosDbService.GetMultipleTransactionsAsync($"SELECT * FROM c WHERE c.userid = {userid}"));
        }
    }
}

