using Authentication.Net.Application.Interfaces;
using Authentication.Net.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizationMiddleware : TypeFilterAttribute
{
	public AuthorizationMiddleware() : base(typeof(AuthorizationMiddlewareFilter))
	{
	}
}

public class AuthorizationMiddlewareFilter : IAsyncActionFilter
{
	private readonly IJwtProvider _jwtProvider;

	public AuthorizationMiddlewareFilter(IJwtProvider jwtProvider)
	{
		_jwtProvider = jwtProvider;
	}

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		try
		{
			string authorization = context.HttpContext.Request.Headers["Authorization"];
			if (string.IsNullOrEmpty(authorization) || !_jwtProvider.IsTokenValid(authorization))
			{
				var unauthorizedResult = new ObjectResult("Invalid or missing JWT token")
				{
					StatusCode = 401
				};
				context.Result = unauthorizedResult;
				return;
			}
			else
			{
				await next();
			}
		}
		catch (InternalErrorException)
		{
			throw new Exception("Internal Error, contact the support.");
		}
	}
}
