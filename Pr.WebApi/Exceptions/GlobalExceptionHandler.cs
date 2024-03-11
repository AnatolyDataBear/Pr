using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Pr.WebApi.Exceptions
{
	internal sealed class GlobalExceptionHandler : IExceptionHandler
	{
		private readonly ILogger<GlobalExceptionHandler> _logger;

		public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
		{
			_logger = logger;
		}

		public async ValueTask<bool> TryHandleAsync(
			HttpContext httpContext,
			Exception exception,
			CancellationToken cancellationToken)
		{			
			_logger.LogError(
				exception, "Error: {Message}", exception.Message);
			
			var problemDetails = new ProblemDetails
			{
				Detail = exception.Message
			};

			if (httpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
			{
				problemDetails.Status = StatusCodes.Status400BadRequest;
				problemDetails.Title = "Bad request";
			}
			else
			if (httpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
			{
				problemDetails.Status = StatusCodes.Status403Forbidden;
				problemDetails.Title = "Forbidden";
			}
			else
			{
				problemDetails.Status = StatusCodes.Status500InternalServerError;
				problemDetails.Title = "Internal error";
			}

			httpContext.Response.StatusCode = problemDetails.Status.Value;

			await httpContext.Response
				.WriteAsJsonAsync(problemDetails, cancellationToken);

			return true;
		}
	}
}
