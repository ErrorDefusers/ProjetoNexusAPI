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
        public IActionResult Login([FromBody] LoginDTO login)
        {
            try
            {
                
                var usuario = _context.Funcionarios
                    .FirstOrDefault(f => f.Email.ToLower() == login.Email!.ToLower() && f.Senha == login.Password);

                if (usuario == null)
                    return Unauthorized("Email ou senha inválidos.");

                
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.IdFuncionario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                    new Claim("nome", usuario.Nome)
                };

                
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:DurationInMinutes"]!)),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    nome = usuario.Nome,
                    email = usuario.Email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao fazer login: " + ex.Message);
            }
        }
    }
}
