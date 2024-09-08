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
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(ClientService clientService, ILogger<ClientsController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpGet("all", Name = "GetAllClients")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ClientInfoView>?>> All()
        {
            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                if (clients == null || clients.Count == 0)
                {
                    _logger.LogWarning("No clients found.");
                    return ApiResponseHelper.NotFound("No clients found.");
                }
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all clients.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("findByClientId/{id:int}", Name = "FindClientByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDetailsDto?>> FindByClientId([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ID must be greater than zero: {Id}", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var client = await _clientService.FindByClientIdAsync(id);
                if (client == null)
                {
                    _logger.LogWarning("Client with ID {Id} not found.", id);
                    return ApiResponseHelper.NotFound($"Client with ID {id} not found.");
                }
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding client by clientId: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("findByPersonId/{personId:int}", Name = "FindClientByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDetailsDto?>> FindByPersonId([FromRoute] int personId)
        {
            if (personId <= 0)
            {
                _logger.LogWarning("Person ID must be greater than zero: {PersonId}", personId);
                return ApiResponseHelper.BadRequest("Person ID must be greater than zero.");
            }

            try
            {
                var client = await _clientService.FindByPersonIdAsync(personId);
                if (client == null)
                {
                    _logger.LogWarning("Client with Person ID {PersonId} not found.", personId);
                    return ApiResponseHelper.NotFound($"Client with Person ID {personId} not found.");
                }
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding client by personId: {PersonId}", personId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("", Name = "AddClient")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDetailsDto?>> Add([FromBody] CreateOrUpdateClientDto newClientDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state while adding new client.");
                return BadRequest(ModelState);
            }

            if (newClientDto == null)
            {
                _logger.LogWarning("Client data is required.");
                return ApiResponseHelper.BadRequest("Client data is required.");
            }

            try
            {
                var client = await _clientService.AddClientAsync(newClientDto);
                return ApiResponseHelper.HandleNull(client, "Failed to add new client.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding new client.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}", Name = "UpdateClient")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDetailsDto?>> Update([FromRoute] int id, [FromBody] CreateOrUpdateClientDto updatedClientDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state while updating client with Id: {Id}", id);
                return BadRequest(ModelState);
            }

            if (updatedClientDto == null)
            {
                _logger.LogWarning("Client data is required for update.");
                return ApiResponseHelper.BadRequest("Client data is required.");
            }

            if (id <= 0)
            {
                _logger.LogWarning("ID must be greater than zero: {Id}", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var client = await _clientService.UpdateClientAsync(id, updatedClientDto);
                return ApiResponseHelper.HandleNull(client, "Failed to update client.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating client with Id: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteClient")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ID must be greater than zero: {Id}", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var isDeleted = await _clientService.DeleteClientAsync(id);
                if (!isDeleted)
                {
                    _logger.LogWarning("Client with ID {Id} not found for deletion.", id);
                    return ApiResponseHelper.NotFound($"Client with ID {id} not found.");
                }
                return ApiResponseHelper.Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting client with Id: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("existsByClientId/{id:int}", Name = "ExistsByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExistsByClientId([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ID must be greater than zero: {Id}", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var isFound = await _clientService.ExistsByClientIdAsync(id);
                return ApiResponseHelper.Ok(isFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of client by clientId: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("existsByPersonId/{personId:int}", Name = "ExistsByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExistsByPersonId([FromRoute] int personId)
        {
            if (personId <= 0)
            {
                _logger.LogWarning("Person ID must be greater than zero: {PersonId}", personId);
                return ApiResponseHelper.BadRequest("Person ID must be greater than zero.");
            }

            try
            {
                var isFound = await _clientService.ExistsByPersonIdAsync(personId);
                return ApiResponseHelper.Ok(isFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of client by personId: {PersonId}", personId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
