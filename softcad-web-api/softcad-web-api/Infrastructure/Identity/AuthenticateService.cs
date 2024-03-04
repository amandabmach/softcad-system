using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiOperacaoCuriosidade.Domain.Account;
using WebApiOperacaoCuriosidade.Domain.Models;
using WebApiOperacaoCuriosidade.Infrastructure.Repository.Interfaces;

namespace WebApiOperacaoCuriosidade.Infrastructure.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly IAdministradorRepository _adminRepository;
        private readonly IConfiguration _configuration;

        public AuthenticateService(IAdministradorRepository repository ,IConfiguration configuration)
        {
            _adminRepository = repository;
            _configuration = configuration;
        }

        public bool Authenticate(string email, string password)
        {

            var administrador = _adminRepository.GetAll().Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            if (administrador == null)
            {
                return false;
            }

            using var hmac = new HMACSHA512(administrador.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int x = 0; x < computedHash.Length; x++)
            {
                if (computedHash[x] != administrador.PasswordHash[x]) return false;
            }

            return true;
        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email.ToLower()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_configuration["jwt:secretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Administrator GetAdminByEmail(string email)
        {
            return _adminRepository.GetAll().Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public bool AdminExists(string email)
        {
            var usuario = _adminRepository.GetAll().Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            if (usuario == null)
            {
                return false;
            }

            return true;
        }

      
    }
}
