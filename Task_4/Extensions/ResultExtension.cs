using Microsoft.AspNetCore.Mvc;

namespace Task_4.Extensions
{
    public static class ResultExtension
    {
        public static ActionResult<T> EntityNotFound<T>(int id, string entityName)
        {
            return new NotFoundObjectResult(new
            {
                message = $"{entityName} with ID {id} not found"
            });
        }

        public static ActionResult EntityBadRequest(string message)
        {
            return new BadRequestObjectResult(new
            {
                message = message
            });
        }
    }
}
