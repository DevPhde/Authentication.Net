using Microsoft.AspNetCore.Mvc.Filters;

namespace Authentication.Net.API.Middleware
{
	public class AuthorizationMiddleware : ActionFilterAttribute
	{
        public override void OnActionExecuting(ActionExecutingContext context)
		{
			string authorization = context.HttpContext.Request.Headers["authorization"];
			if(string.IsNullOrEmpty(authorization))
			{
				Console.WriteLine("empty");
			}
            Console.WriteLine("Middlware");
			var header = context.HttpContext.Request.Headers["authorization"];
            Console.WriteLine(header);
			base.OnActionExecuting(context);
		}
	}
}
