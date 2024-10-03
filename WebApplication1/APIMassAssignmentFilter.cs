using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public class APIMassAssignmentFilter : IAsyncActionFilter
    {
        private readonly string[] _forbiddenWords = new string[] { "isadmin", "issso", "role", "is_admin", "is_sso" };

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (context.ActionArguments.Count > 0)
            {
                var argument = context.ActionArguments.FirstOrDefault().Value;

                if (argument != null)
                {
                    var disallowedFieldsFound = _forbiddenWords
                        .Where(field => argument.GetType().GetProperty(field) != null)
                        .ToArray();

                    if (disallowedFieldsFound.Any())
                    {
                        context.Result = new BadRequestObjectResult($"Object contains forbidden fields: " +
                            $"{string.Join(", ", disallowedFieldsFound)}");
                        return;
                    }
                }
            }

            await next();
        }
    }
}
