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
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("all", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserDetailsDto>?>> All()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                if (users == null || users.Count == 0)
                {
                    _logger.LogWarning("No users found.");
                }
                return ApiResponseHelper.HandleNull(users, "Failed to fetch users.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all users");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("findByUserId/{id:int}", Name = "FindUserByUserId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDetailsDto?>> FindByUserId([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid user ID: {Id}. Must be greater than zero.", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var user = await _userService.FindByUserIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {Id} not found.", id);
                }
                return ApiResponseHelper.HandleNull(user, $"Failed to find user with ID {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding user by ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("findByPersonId/{personId:int}", Name = "FindUserByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDetailsDto?>> FindByPersonId([FromRoute] int personId)
        {
            if (personId <= 0)
            {
                _logger.LogWarning("Invalid person ID: {PersonId}. Must be greater than zero.", personId);
                return ApiResponseHelper.BadRequest("Person ID must be greater than zero.");
            }

            try
            {
                var user = await _userService.FindByPersonIdAsync(personId);
                if (user == null)
                {
                    _logger.LogWarning("User with Person ID {PersonId} not found.", personId);
                }
                return ApiResponseHelper.HandleNull(user, $"Failed to find user with Person ID {personId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding user by Person ID: {PersonId}", personId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("findByUsername/{username}", Name = "FindUserByUsername")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDetailsDto?>> FindByUsername([FromRoute] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                _logger.LogWarning("Username is required for finding user.");
                return ApiResponseHelper.BadRequest("Username is required.");
            }

            try
            {
                var user = await _userService.FindByUsernameAsync(username);
                if (user == null)
                {
                    _logger.LogWarning("User with username {Username} not found.", username);
                }
                return ApiResponseHelper.HandleNull(user, $"Failed to find user with username {username}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding user by username: {Username}", username);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("findByUsernameAndPassword/{username}/{password}", Name = "FindUserByUsernameAndPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDetailsDto?>> FindByUsernameAndPassword([FromRoute] string username, [FromRoute] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                _logger.LogWarning("Username and password are required for finding user.");
                return ApiResponseHelper.BadRequest("Username and password are required.");
            }

            try
            {
                var user = await _userService.FindByUsernameAndPasswordAsync(username, password);
                if (user == null)
                {
                    _logger.LogWarning("User with username {Username} and provided password not found.", username);
                }
                return ApiResponseHelper.HandleNull(user, "Failed to find user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding user by username: {Username} and password.", username);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDetailsDto?>> Add([FromBody] CreateOrUpdateUserDto newUserDto)
        {
            if (newUserDto == null)
            {
                _logger.LogWarning("User data is required to add a new user.");
                return ApiResponseHelper.BadRequest("User data is required.");
            }

            try
            {
                var user = await _userService.AddUserAsync(newUserDto);
                if (user == null)
                {
                    _logger.LogWarning("Failed to add new user.");
                }
                return ApiResponseHelper.HandleNull(user, "Failed to add new user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding new user");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDetailsDto?>> Update([FromRoute] int id, [FromBody] CreateOrUpdateUserDto updatedUserDto)
        {
            if (id <= 0 || updatedUserDto == null)
            {
                _logger.LogWarning("Invalid ID: {Id} or missing user data.", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero and user data is required.");
            }

            try
            {
                var user = await _userService.UpdateUserAsync(id, updatedUserDto);
                if (user == null)
                {
                    _logger.LogWarning("Failed to update user with ID: {Id}", id);
                }
                return ApiResponseHelper.HandleNull(user, "Failed to update user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID: {Id}. Must be greater than zero.", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var isDeleted = await _userService.DeleteUserAsync(id);
                if (!isDeleted)
                {
                    _logger.LogWarning("User with ID {Id} not found or could not be deleted.", id);
                    return NotFound(false);
                }

                return ApiResponseHelper.Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("existsByUserId/{id:int}", Name = "ExistsUserByUserId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExistsByUserId([FromRoute] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid user ID: {Id}. Must be greater than zero.", id);
                return ApiResponseHelper.BadRequest("ID must be greater than zero.");
            }

            try
            {
                var userExists = await _userService.ExistsByUserIdAsync(id);
                return ApiResponseHelper.Ok(userExists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of user with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("existsByPersonId/{personId:int}", Name = "ExistsUserByPersonId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExistsByPersonId([FromRoute] int personId)
        {
            if (personId <= 0)
            {
                _logger.LogWarning("Invalid person ID: {PersonId}. Must be greater than zero.", personId);
                return ApiResponseHelper.BadRequest("Person ID must be greater than zero.");
            }

            try
            {
                var userExists = await _userService.ExistsByPersonIdAsync(personId);
                return ApiResponseHelper.Ok(userExists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of user with Person ID: {PersonId}", personId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("existsByUsername/{username}", Name = "ExistsUserByUsername")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExistsByUsername([FromRoute] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                _logger.LogWarning("Username is required to check existence.");
                return ApiResponseHelper.BadRequest("Username is required.");
            }

            try
            {
                var userExists = await _userService.ExistsByUsernameAsync(username);
                return ApiResponseHelper.Ok(userExists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of user with username: {Username}", username);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("existsByUsernameAndPassword/{username}/{password}", Name = "ExistsUserByUsernameAndPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExistsByUsernameAndPassword([FromRoute] string username, [FromRoute] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                _logger.LogWarning("Username and password are required to check existence.");
                return ApiResponseHelper.BadRequest("Username and password are required.");
            }

            try
            {
                var userExists = await _userService.ExistsByUsernameAndPasswordAsync(username, password);
                return ApiResponseHelper.Ok(userExists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of user with username: {Username} and password.", username);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

    }
}
