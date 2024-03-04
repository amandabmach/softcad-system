using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using softcad_web_api.Infrastructure.Configuration;
using WebApiOperacaoCuriosidade.Application.Services;
using WebApiOperacaoCuriosidade.Domain.DTOs;


namespace WebApiOperacaoCuriosidade.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Insert([FromBody]UserDTO usuarioDTO)
        {
            _service.Insert(usuarioDTO, User.GetId());

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _service.GetAll(User.GetId());

            return Ok(list);
        }

        [HttpGet("administrator")]
        public IActionResult GetUsersByAdmin()
        {
            var list = _service.GetUsersByAdmin(User.GetId());

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _service.GetById(id, User.GetId());

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id, User.GetId());

            return NoContent();
        }

        [HttpPut]
        public IActionResult Update([FromBody]UserDTO usuarioDTO)
        {

            _service.Update(usuarioDTO, User.GetId());

            return NoContent();
           
        }

    }
}
