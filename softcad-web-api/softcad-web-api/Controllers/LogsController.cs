using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiOperacaoCuriosidade.Application.Services;
using WebApiOperacaoCuriosidade.Application.Services.Impl;
using WebApiOperacaoCuriosidade.Application.Services.Interfaces;
using WebApiOperacaoCuriosidade.Infrastructure;

namespace WebApiOperacaoCuriosidade.Controllers
{
    [Authorize]
    [ApiController]
    [Route("logs")]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _service;
        public LogsController(ILogService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var list = _service.GetLogs(User.GetId());

            return Ok(list);
        }

    }
}
