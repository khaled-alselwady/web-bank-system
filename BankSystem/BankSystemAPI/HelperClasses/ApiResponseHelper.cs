using Microsoft.AspNetCore.Mvc;

namespace BankSystemAPI.HelperClasses
{
    public static class ApiResponseHelper
    {
        public static ActionResult NotFound(string? message = null)
            => new NotFoundObjectResult(new { message = message ?? ApiMessages.NotFound });

        public static ActionResult BadRequest(string? message = null)
            => new BadRequestObjectResult(new { message = message ?? ApiMessages.InvalidData });

        public static ActionResult Ok<T>(T data)
            => new OkObjectResult(data);

        public static ActionResult HandleNull<T>(T data, string? errorMessage = null)
            => data == null ? NotFound(errorMessage) : Ok(data);

        public static ActionResult OperationFailed(string? message = null)
            => BadRequest(message ?? ApiMessages.OperationFailed);

    }
}

