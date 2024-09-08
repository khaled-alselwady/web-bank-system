using BankSystem.Business.Services;
using BankSystem.DTOs.UserDTOs;
using BankSystemAPI.HelperClasses;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserDetailsDto>?>> All()
        {
            var users = await _userService.AllAsync();

            return ApiResponseHelper.HandleNull(users, "Failed to fetch users.");
        }

        [HttpGet("findByUserId/{id:int}", Name = "FindUserByUserId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDto?>> FindByUserId([FromRoute] int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.FindByUserIdAsync(id);

            return ApiResponseHelper.HandleNull(user, $"Failed to find user with ID {id}.");
        }

        [HttpGet("findByPersonId/{personId:int}", Name = "FindUserByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDto?>> FindByPersonId([FromRoute] int personId)
        {
            if (personId <= 0)
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.FindByPersonIdAsync(personId);

            return ApiResponseHelper.HandleNull(user, $"Failed to find user with Person ID {personId}.");
        }

        [HttpGet("findByUsername/{username}", Name = "FindUserByUsername")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDto?>> FindByUsername([FromRoute] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.FindByUsernameAsync(username);

            return ApiResponseHelper.HandleNull(user, $"Failed to find user with username {username}.");
        }

        [HttpGet("findByUsernameAndPassword/{username}/{password}", Name = "FindUserByUsernameAndPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDto?>> FindByUsernameAndPassword([FromRoute] string username, [FromRoute] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.FindByUsernameAndPasswordAsync(username, password);

            return ApiResponseHelper.HandleNull(user, $"Failed to find user.");
        }

        [HttpPost("", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDto?>> Add([FromBody] CreateOrUpdateUserDto newUserDto)
        {
            if (newUserDto == null)
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.AddAsync(newUserDto);

            return ApiResponseHelper.HandleNull(user, "Failed to add new user.");
        }

        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDto?>> Update([FromRoute] int id, [FromBody] CreateOrUpdateUserDto updatedUserDto)
        {
            if (id <= 0 || updatedUserDto == null)
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.UpdateAsync(id, updatedUserDto);

            return ApiResponseHelper.HandleNull(user, "Failed to add new user.");
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest();
            }

            var isDeleted = await _userService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound(false);
            }

            return ApiResponseHelper.Ok(true);
        }

        [HttpGet("existsByUserId/{id:int}", Name = "ExistsUserByUserId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> ExistsByUserId([FromRoute] int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.ExistsByUserId(id);

            return ApiResponseHelper.HandleNull(user, $"No user with ID {id}.");
        }

        [HttpGet("existsByPersonId/{personId:int}", Name = "ExistsUserByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> ExistsByPersonId([FromRoute] int personId)
        {
            if (personId <= 0)
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.ExistsByPersonId(personId);

            return ApiResponseHelper.HandleNull(user, $"No user with Person ID {personId}.");
        }

        [HttpGet("existsByUsername/{username}", Name = "ExistsUserByUsername")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> ExistsByUsername([FromRoute] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.ExistsByUsername(username);

            return ApiResponseHelper.HandleNull(user, $"No user with username {username}.");
        }

        [HttpGet("existsByUsernameAndPassword/{username}/{password}", Name = "ExistsUserByUsernameAndPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> ExistsByUsernameAndPassword([FromRoute] string username, [FromRoute] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return ApiResponseHelper.BadRequest();
            }

            var user = await _userService.ExistsByUsernameAndPassword(username, password);

            return ApiResponseHelper.HandleNull(user, $"Not user found");
        }

    }
}
