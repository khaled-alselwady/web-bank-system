using BankSystemAPI.HelperClasses;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;

        protected BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected async Task<ActionResult<T>> HandleRequestAsync<T>(Func<Task<T>> func, string errorMessage)
        {
            try
            {
                var result = await func();
                if (result == null)
                {
                    _logger.LogWarning(errorMessage);
                    return ApiResponseHelper.HandleNull(result, errorMessage);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        protected ActionResult HandleBadRequest(string errorMessage)
        {
            _logger.LogWarning(errorMessage);
            return ApiResponseHelper.BadRequest(errorMessage);
        }
    }
}
