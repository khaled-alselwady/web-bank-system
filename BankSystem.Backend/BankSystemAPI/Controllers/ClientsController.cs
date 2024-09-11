using BankSystem.Business.Services;
using BankSystem.DTOs.ClientDTOs;
using BankSystemDataAccess.Entities.Views;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : BaseController
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService, ILogger<ClientsController> logger)
            : base(logger)
        {
            _clientService = clientService;
        }

        [HttpGet("all", Name = "GetAllClients")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<List<ClientInfoView>?>> All() =>
            HandleRequestAsync(() => _clientService.GetAllClientsAsync(), "Failed to fetch clients.");

        [HttpGet("findByClientId/{id:int}", Name = "FindClientByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<ClientDetailsDto?>> FindByClientId([FromRoute] int id) =>
            id > 0
                ? HandleRequestAsync(() => _clientService.FindByClientIdAsync(id), $"Failed to find client with ID {id}.")
                : Task.FromResult<ActionResult<ClientDetailsDto?>>(HandleBadRequest("ID must be greater than zero."));

        [HttpGet("findByPersonId/{personId:int}", Name = "FindClientByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<ClientDetailsDto?>> FindByPersonId([FromRoute] int personId) =>
            personId > 0
                ? HandleRequestAsync(() => _clientService.FindByPersonIdAsync(personId), $"Failed to find client with Person ID {personId}.")
                : Task.FromResult<ActionResult<ClientDetailsDto?>>(HandleBadRequest("Person ID must be greater than zero."));

        [HttpPost("", Name = "AddClient")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<ClientDetailsDto?>> Add([FromBody] CreateOrUpdateClientDto newClientDto) =>
            newClientDto != null
                ? HandleRequestAsync(() => _clientService.AddClientAsync(newClientDto), "Failed to add new client.")
                : Task.FromResult<ActionResult<ClientDetailsDto?>>(HandleBadRequest("Client data is required."));

        [HttpPut("{id:int}", Name = "UpdateClient")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<ClientDetailsDto?>> Update([FromRoute] int id, [FromBody] CreateOrUpdateClientDto updatedClientDto) =>
            id > 0 && updatedClientDto != null
                ? HandleRequestAsync(() => _clientService.UpdateClientAsync(id, updatedClientDto), "Failed to update client.")
                : Task.FromResult<ActionResult<ClientDetailsDto?>>(HandleBadRequest("ID must be greater than zero and client data is required."));

        [HttpDelete("{id:int}", Name = "DeleteClient")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> Delete([FromRoute] int id) =>
            id > 0
                ? HandleRequestAsync(() => _clientService.DeleteClientAsync(id), "Failed to delete client.")
                : Task.FromResult<ActionResult<bool>>(HandleBadRequest("ID must be greater than zero."));

        [HttpGet("existsByClientId/{id:int}", Name = "ExistsClientByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> ExistsByClientId([FromRoute] int id) =>
            id > 0
                ? HandleRequestAsync(() => _clientService.ExistsByClientIdAsync(id), $"Error occurred while checking existence of client with ID: {id}.")
                : Task.FromResult<ActionResult<bool>>(HandleBadRequest("ID must be greater than zero."));

        [HttpGet("existsByPersonId/{personId:int}", Name = "ExistsClientByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> ExistsByPersonId([FromRoute] int personId) =>
            personId > 0
                ? HandleRequestAsync(() => _clientService.ExistsByPersonIdAsync(personId), $"Error occurred while checking existence of client with Person ID: {personId}.")
                : Task.FromResult<ActionResult<bool>>(HandleBadRequest("Person ID must be greater than zero."));

        [HttpGet("count", Name = "CountClients")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<int>> Count() =>
             HandleRequestAsync(() => _clientService.CountClientsAsync(), "Error occurred while count the clients.");
    }
}
