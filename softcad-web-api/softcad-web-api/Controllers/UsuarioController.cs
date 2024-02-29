using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiOperacaoCuriosidade.Application.Services;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Infrastructure;


namespace WebApiOperacaoCuriosidade.Controllers
{
    [Authorize]
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult Insert([FromBody]UsuarioDTO usuarioDTO)
        {
            _usuarioService.Insert(usuarioDTO, User.GetId());

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _usuarioService.GetAll(User.GetId());

            return Ok(list);
        }

        [HttpGet("administrador")]
        public IActionResult GetUsersByAdmin()
        {
            var list = _usuarioService.GetUsersByAdmin(User.GetId());

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _usuarioService.GetById(id, User.GetId());

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _usuarioService.Delete(id, User.GetId());

            return NoContent();
        }

        [HttpPut]
        public IActionResult Update([FromBody]UsuarioDTO usuarioDTO)
        {

            _usuarioService.Update(usuarioDTO, User.GetId());

            return NoContent();
           
        }

    }
}
