using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NexusAPI.Domains;
using NexusAPI.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly NexusContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(NexusContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var usuario = _context.Funcionarios.FirstOrDefault(u =>
                    u.Email == loginDto.Email && u.Senha == loginDto.Password);

                if (usuario == null)
                    return Unauthorized("Email ou senha inválidos!");

                // Geração do token com role
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Email, usuario.Email),
                        new Claim(ClaimTypes.Role, usuario.Role) // 👈 Inclui o nível de acesso
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    role = usuario.Role,
                    nome = usuario.Nome
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao fazer login: " + ex.Message);
            }
        }
    }
}
