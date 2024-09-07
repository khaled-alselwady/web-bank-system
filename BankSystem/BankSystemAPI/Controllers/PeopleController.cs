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

        public PeopleController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("all", Name = "GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PersonDetailsDto>>> All()
        {
            var people = await _personService.AllAsync();
            return ApiResponseHelper.HandleNull(people, "No people found.");
        }

        [HttpGet("{id:int}", Name = "FindPerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonDetailsDto>> Find([FromRoute] int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            var person = await _personService.FindAsync(id);
            return ApiResponseHelper.HandleNull(person, $"Person with ID {id} not found.");
        }

        [HttpPost("", Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonDetailsDto?>> Add([FromBody] CreateOrUpdatePersonDto newPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newPerson == null)
            {
                return ApiResponseHelper.BadRequest("Person data is required.");
            }

            var person = await _personService.AddAsync(newPerson);
            return ApiResponseHelper.HandleNull(person, "Failed to add new person.");
        }

        [HttpPut("{id:int}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonDetailsDto?>> Update([FromRoute] int id, [FromBody] CreateOrUpdatePersonDto updatePersonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            var person = await _personService.UpdateAsync(id, updatePersonDto);

            return ApiResponseHelper.HandleNull(person, "Failed to update person.");
        }

        [HttpDelete("{id:int}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            var isDeleted = await _personService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound(false);
            }

            return ApiResponseHelper.Ok(true);
        }

        [HttpGet("exists/{id:int}", Name = "DoesExistPerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Exists([FromRoute] int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            var isFound = await _personService.ExistsAsync(id);

            return ApiResponseHelper.Ok(isFound);
        }
    }
}
