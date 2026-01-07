using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;

namespace Middleware
{
    public class CustomAuthorizationResultHandler
        : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = 403,
                    error = "forbidden",
                    message = "Admin access required"
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
                return;
            }

            if (authorizeResult.Challenged)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = 401,
                    error = "unauthorized",
                    message = "Authentication required"
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
                return;
            }

            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
