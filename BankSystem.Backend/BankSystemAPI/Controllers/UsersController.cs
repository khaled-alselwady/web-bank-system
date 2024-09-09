using BankSystem.Business.Services;
using BankSystem.DTOs.UserDTOs;
using BankSystemDataAccess.Entities.Views;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly UserService _userService;

        public UsersController(UserService userService, ILogger<UsersController> logger) : base(logger)
        {
            _userService = userService;
        }

        [HttpGet("all", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<List<UserInfoView>?>> All() =>
            HandleRequestAsync(() => _userService.GetAllUsersAsync(), "Failed to fetch users.");

        [HttpGet("findByUserId/{id:int}", Name = "FindUserByUserId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<UserDetailsDto?>> FindByUserId([FromRoute] int id) =>
            id > 0
                ? HandleRequestAsync(() => _userService.FindByUserIdAsync(id), $"Failed to find user with ID {id}.")
                : Task.FromResult<ActionResult<UserDetailsDto?>>(HandleBadRequest("ID must be greater than zero."));

        [HttpGet("findByPersonId/{personId:int}", Name = "FindUserByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<UserDetailsDto?>> FindByPersonId([FromRoute] int personId) =>
            personId > 0
                ? HandleRequestAsync(() => _userService.FindByPersonIdAsync(personId), $"Failed to find user with Person ID {personId}.")
                : Task.FromResult<ActionResult<UserDetailsDto?>>(HandleBadRequest("Person ID must be greater than zero."));

        [HttpGet("findByUsername/{username}", Name = "FindUserByUsername")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<UserDetailsDto?>> FindByUsername([FromRoute] string username) =>
            !string.IsNullOrWhiteSpace(username)
                ? HandleRequestAsync(() => _userService.FindByUsernameAsync(username), $"Failed to find user with username {username}.")
                : Task.FromResult<ActionResult<UserDetailsDto?>>(HandleBadRequest("Username is required."));

        [HttpGet("findByUsernameAndPassword/{username}/{password}", Name = "FindUserByUsernameAndPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<UserDetailsDto?>> FindByUsernameAndPassword([FromRoute] string username, [FromRoute] string password) =>
            !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password)
                ? HandleRequestAsync(() => _userService.FindByUsernameAndPasswordAsync(username, password), "Failed to find user.")
                : Task.FromResult<ActionResult<UserDetailsDto?>>(HandleBadRequest("Username and password are required."));

        [HttpPost("", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<UserDetailsDto?>> Add([FromBody] CreateOrUpdateUserDto newUserDto) =>
            newUserDto != null
                ? HandleRequestAsync(() => _userService.AddUserAsync(newUserDto), "Failed to add new user.")
                : Task.FromResult<ActionResult<UserDetailsDto?>>(HandleBadRequest("User data is required."));

        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<UserDetailsDto?>> Update([FromRoute] int id, [FromBody] CreateOrUpdateUserDto updatedUserDto) =>
            id > 0 && updatedUserDto != null
                ? HandleRequestAsync(() => _userService.UpdateUserAsync(id, updatedUserDto), "Failed to update user.")
                : Task.FromResult<ActionResult<UserDetailsDto?>>(HandleBadRequest("ID must be greater than zero and user data is required."));

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> Delete([FromRoute] int id) =>
            id > 0
            ? HandleRequestAsync(() => _userService.DeleteUserAsync(id), "Failed to delete user.")
            : Task.FromResult<ActionResult<bool>>(HandleBadRequest("ID must be greater than zero."));

        [HttpGet("existsByUserId/{id:int}", Name = "ExistsUserByUserId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> ExistsByUserId([FromRoute] int id) =>
            id > 0
                ? HandleRequestAsync(() => _userService.ExistsByUserIdAsync(id), $"Error occurred while checking existence of user with ID: {id}")
                : Task.FromResult<ActionResult<bool>>(HandleBadRequest("ID must be greater than zero."));

        [HttpGet("existsByPersonId/{personId:int}", Name = "ExistsUserByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> ExistsByPersonId([FromRoute] int personId) =>
            personId > 0
                ? HandleRequestAsync(() => _userService.ExistsByPersonIdAsync(personId), $"Error occurred while checking existence of user with Person ID: {personId}")
                : Task.FromResult<ActionResult<bool>>(HandleBadRequest("Person ID must be greater than zero."));

        [HttpGet("existsByUsername/{username}", Name = "ExistsUserByUsername")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> ExistsByUsername([FromRoute] string username) =>
            !string.IsNullOrWhiteSpace(username)
                ? HandleRequestAsync(() => _userService.ExistsByUsernameAsync(username), $"Error occurred while checking existence of user with username: {username}")
                : Task.FromResult<ActionResult<bool>>(HandleBadRequest("Username is required."));

        [HttpGet("existsByUsernameAndPassword/{username}/{password}", Name = "ExistsUserByUsernameAndPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<ActionResult<bool>> ExistsByUsernameAndPassword([FromRoute] string username, [FromRoute] string password) =>
            !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password)
                ? HandleRequestAsync(() => _userService.ExistsByUsernameAndPasswordAsync(username, password), $"Error occurred while checking existence of user with username: {username} and password.")
                : Task.FromResult<ActionResult<bool>>(HandleBadRequest("Username and password are required."));

        [HttpGet("isActive/{id:int}", Name = "IsUserActive")]
        public Task<ActionResult<bool>> IsUserActive([FromRoute] int id) =>
        id > 0
             ? HandleRequestAsync(() => _userService.IsUserActive(id), $"Error occurred while checking the activity of the user with ID: {id}.")
             : Task.FromResult<ActionResult<bool>>(HandleBadRequest("ID must be greater than zero."));
    }
}
