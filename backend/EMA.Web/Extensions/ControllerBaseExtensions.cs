using Microsoft.AspNetCore.Mvc;

namespace EMA.Web.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult InternalServerError(this ControllerBase controller, string message = "Internal Server Error")
        {
            return controller.StatusCode(StatusCodes.Status500InternalServerError, message);
        }

        public static ActionResult<T> InternalServerError<T>(this ControllerBase controller, string message = "Internal Server Error")
        {
            return controller.StatusCode(StatusCodes.Status500InternalServerError, message);
        }
    }
}
