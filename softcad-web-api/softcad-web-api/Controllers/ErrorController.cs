using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApiOperacaoCuriosidade.Application.Services;
using WebApiOperacaoCuriosidade.Application.Services.Exceptions;
using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogService _logService;

        public ErrorController(ILogService logService)
        {
            _logService = logService;
        }

        [Route("/error")]
        public IActionResult Error()
        {
         
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; 

            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "Ocorreu um erro inesperado.";
            var path = HttpContext.Request.Path;

            if (exception is RegraNegocioException e)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = exception.Message;
                _logService.RegisterLog(new Logs(e.ExecutorId, e.Operation, e.Timestamp, e.Message, e.Result));

            }
            else if (exception is Exception)
            {
                statusCode = StatusCodes.Status500InternalServerError;
                message = exception.Message;
            }

            var errorResponse = new StandardError
            {
                Timestamp = DateTime.Now,
                Status = statusCode,
                Error = message,
                Message = exception?.Message,
                Path = path
            };

            return StatusCode(statusCode, errorResponse);

        }
    }
}
