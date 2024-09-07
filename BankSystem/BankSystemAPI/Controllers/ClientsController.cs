using BankSystem.Business.Services;
using BankSystem.DTOs.ClientDTOs;
using BankSystemAPI.HelperClasses;
using BankSystemDataAccess.Entities.Views;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("all", Name = "GetAllClients")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ClientInfoView>?>> All()
        {
            var client = await _clientService.AllAsync();

            if (client == null)
            {
                return ApiResponseHelper.NotFound();
            }

            return Ok(client);
        }

        [HttpGet("findByClientId/{id:int}", Name = "FindClientByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ClientDetailsDto?>> FindByClientId([FromRoute] int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest();
            }

            var client = await _clientService.FindByClientIdAsync(id);

            if (client == null)
            {
                return ApiResponseHelper.NotFound();
            }

            return Ok(client);
        }

        [HttpGet("findByPersonId/{personId:int}", Name = "FindClientByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ClientDetailsDto?>> FindByPersonId([FromRoute] int personId)
        {
            if (personId <= 0)
            {
                return ApiResponseHelper.BadRequest();
            }

            var client = await _clientService.FindByPersonIdAsync(personId);

            if (client == null)
            {
                return ApiResponseHelper.NotFound();
            }

            return Ok(client);
        }

        [HttpPost("", Name = "AddClient")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ClientDetailsDto?>> Add([FromBody] CreateOrUpdateClientDto newClientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newClientDto == null)
            {
                return ApiResponseHelper.BadRequest("Client data is required.");
            }

            var client = await _clientService.AddAsync(newClientDto);

            return ApiResponseHelper.HandleNull(client, "Failed to add new client.");
        }

        [HttpPut("{id:int}", Name = "UpdateClient")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ClientDetailsDto?>> Update([FromRoute] int id, [FromBody] CreateOrUpdateClientDto updatedClientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updatedClientDto == null)
            {
                return ApiResponseHelper.BadRequest("Client data is required.");
            }

            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            var client = await _clientService.UpdateAsync(id, updatedClientDto);
            return ApiResponseHelper.HandleNull(client, "Failed to update client.");
        }

        [HttpDelete("{id:int}", Name = "DeleteClient")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            var isDeleted = await _clientService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound(false);
            }

            return ApiResponseHelper.Ok(true);
        }

        [HttpGet("existsByClientId/{id:int}", Name = "ExistsByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> ExistsByClientId([FromRoute] int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            var isFound = await _clientService.ExistsByClientIdAsync(id);

            return ApiResponseHelper.Ok(isFound);
        }

        [HttpGet("existsByPersonId/{personId:int}", Name = "ExistsByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> ExistsByPersonId([FromRoute] int personId)
        {
            if (personId <= 0)
            {
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            var isFound = await _clientService.ExistsByPersonIdAsync(personId);

            return ApiResponseHelper.Ok(isFound);
        }
    }
}
