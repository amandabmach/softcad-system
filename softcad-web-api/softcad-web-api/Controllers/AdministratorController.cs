using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using softcad_web_api.Domain.Models;
using WebApiOperacaoCuriosidade.Application.Services.Interfaces;
using WebApiOperacaoCuriosidade.Domain.Account;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Controllers
{
    [ApiController]
    [Route("administrators")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IAuthenticate _authenticate;

        public AdminController(IAdminService adminService, IAuthenticate authenticate)
        {            
            _adminService = adminService;
            _authenticate = authenticate;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var list = _adminService.GetAll();

            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var admin = _adminService.GetById(id);

            return Ok(admin);
        }

        [HttpPost]
        public ActionResult<AdminToken> Create(AdminDTO adminDTO)
        {
            var adminCreate = _adminService.Insert(adminDTO);

            var token = _authenticate.GenerateToken(Convert.ToInt32(adminCreate.Id), adminCreate.Email);

            return new AdminToken
            {
                Token = token,
            };

        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _adminService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] int id, [FromForm]AdminDTO adminDTO)
        {
            _adminService.Update(id, adminDTO);

            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult UpdatePartial([FromRoute] int id, [FromBody]JsonPatchDocument<AdminDTO> admin)
        {
            _adminService.UpdatePartial(id, admin);

            return NoContent();
        }

        [HttpPost("login")]
        public ActionResult<AdminToken> Authenticator([FromBody]Credentials credentials)
        {
            var admin = _adminService.Authenticator(credentials);

            var token = _authenticate.GenerateToken(Convert.ToInt32(admin.Id), admin.Email);

            return new AdminToken
            {
                Token = token,
            };
        }


        [HttpGet("download/{id}")]
        [Authorize]
        public IActionResult DownloadPhoto(int id)
        {
            var imagemBytes = _adminService.DownloadPhoto(id);

            return File(imagemBytes, "image/*");
           
        }
    }
}
