using Contoso.Banking.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Create a private readonly variable of type ICosmosDbService

        // Create a constructor that takes in an ICosmosDbService and assigns it to the private readonly variable

        // Create a GET method that returns all the users in a list

        // Create a GET method that returns a single user by id

        // Create a POST method that creates a new user

        // Create a PUT method that updates a user by id

        // Create a DELETE method that deletes a user by id

        // Create a GET method that returns a single user by email
    }
}
