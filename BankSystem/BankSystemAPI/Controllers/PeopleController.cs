using BankSystemAPI.HelperClasses;
using BankSystemBusiness.Services;
using BankSystemDTOs.PersonDTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemAPI.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonService _personService;
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(PersonService personService, ILogger<PeopleController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet("all", Name = "GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PersonDetailsDto>>> All()
        {
            try
            {
                var people = await _personService.AllAsync();
                if (people == null || !people.Any())
                {
                    _logger.LogWarning("No people found.");
                    return ApiResponseHelper.NotFound("No people found.");
                }
                return Ok(people);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all people.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}", Name = "FindPerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDetailsDto>> Find([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ID must be greater than zero: {Id}", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var person = await _personService.FindAsync(id);
                if (person == null)
                {
                    _logger.LogWarning("Person with ID {Id} not found.", id);
                    return ApiResponseHelper.NotFound($"Person with ID {id} not found.");
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding person by Id: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("", Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDetailsDto?>> Add([FromBody] CreateOrUpdatePersonDto newPerson)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state while adding new person.");
                return BadRequest(ModelState);
            }

            if (newPerson == null)
            {
                _logger.LogWarning("Person data is required.");
                return ApiResponseHelper.BadRequest("Person data is required.");
            }

            try
            {
                var person = await _personService.AddAsync(newPerson);
                if (person == null)
                {
                    _logger.LogWarning("Failed to add new person.");
                    return ApiResponseHelper.BadRequest("Failed to add new person.");
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding person.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDetailsDto?>> Update([FromRoute] int id, [FromBody] CreateOrUpdatePersonDto updatePersonDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state while updating person with ID: {Id}", id);
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                _logger.LogWarning("ID must be greater than zero: {Id}", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var person = await _personService.UpdateAsync(id, updatePersonDto);
                if (person == null)
                {
                    _logger.LogWarning("Failed to update person with ID {Id}.", id);
                    return ApiResponseHelper.BadRequest("Failed to update person.");
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating person with Id: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}", Name = "DeletePerson")]
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
                var isDeleted = await _personService.DeleteAsync(id);
                if (!isDeleted)
                {
                    _logger.LogWarning("Person with ID {Id} could not be deleted or was not found.", id);
                    return NotFound(false);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting person with Id: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("exists/{id:int}", Name = "DoesExistPerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Exists([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ID must be greater than zero: {Id}", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var isFound = await _personService.ExistsAsync(id);
                return Ok(isFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of person with Id: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
