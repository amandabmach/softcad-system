using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiOperacaoCuriosidade.Application.Services.Interfaces;
using WebApiOperacaoCuriosidade.Infrastructure;

namespace WebApiOperacaoCuriosidade.Controllers
{
    [Authorize]
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("month")]
        public IActionResult AmountByMonth()
        {
            var amount = _service.AmountUsersByMonth(User.GetId());

            return Ok(amount);
        }

        [HttpGet("status")]
        public IActionResult AmountByStatus()
        {
            var amount = _service.AmountUsersByStatus(User.GetId());

            return Ok(amount);
        }

        [HttpGet("admin")]
        public IActionResult AmountByAdmin()
        {
            var amount = _service.AmountUsersByAdmin(User.GetId());

            return Ok(amount);
        }
    }
}
