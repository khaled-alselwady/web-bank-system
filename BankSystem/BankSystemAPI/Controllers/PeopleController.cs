using BankSystem.API.Controllers;
using BankSystemBusiness.Services;
using BankSystemDTOs.PersonDTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemAPI.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : BaseController
    {
        private readonly PersonService _personService;

        public PeopleController(PersonService personService, ILogger<PeopleController> logger)
            : base(logger)
        {
            _personService = personService;
        }

        [HttpGet("all", Name = "GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<List<PersonDetailsDto>?>> All() =>
            HandleRequestAsync(() => _personService.GetAllPeopleAsync(), "No people found.");

        [HttpGet("{id:int}", Name = "FindPerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<PersonDetailsDto?>> Find([FromRoute] int id) =>
            id > 0
                ? HandleRequestAsync(() => _personService.FindByIdAsync(id), $"Person with ID {id} not found.")
                : Task.FromResult<ActionResult<PersonDetailsDto?>>(HandleBadRequest("ID must be greater than zero."));

        [HttpPost("", Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<PersonDetailsDto?>> Add([FromBody] CreateOrUpdatePersonDto newPerson) =>
            newPerson != null
                ? HandleRequestAsync(() => _personService.AddPersonAsync(newPerson), "Failed to add new person.")
                : Task.FromResult<ActionResult<PersonDetailsDto?>>(HandleBadRequest("Person data is required."));

        [HttpPut("{id:int}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<PersonDetailsDto?>> Update([FromRoute] int id, [FromBody] CreateOrUpdatePersonDto updatePersonDto) =>
            id > 0 && updatePersonDto != null
                ? HandleRequestAsync(() => _personService.UpdatePersonAsync(id, updatePersonDto), "Failed to update person.")
                : Task.FromResult<ActionResult<PersonDetailsDto?>>(HandleBadRequest("ID must be greater than zero and person data is required."));

        [HttpDelete("{id:int}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> Delete([FromRoute] int id) =>
            id > 0
                ? HandleRequestAsync(() => _personService.DeletePersonAsync(id), "Failed to delete person.")
                : Task.FromResult<ActionResult<bool>>(HandleBadRequest("ID must be greater than zero."));

        [HttpGet("exists/{id:int}", Name = "DoesExistPerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> Exists([FromRoute] int id) =>
            id > 0
                ? HandleRequestAsync(() => _personService.ExistsByIdAsync(id), $"Error occurred while checking existence of person with ID: {id}.")
                : Task.FromResult<ActionResult<bool>>(HandleBadRequest("ID must be greater than zero."));
    }
}
