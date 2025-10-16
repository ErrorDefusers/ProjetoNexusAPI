using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NexusAPI.Domains;
using NexusAPI.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
                // Procura o funcionário pelo email e senha
                var funcionario = _context.Funcionarios
                    .Include(f => f.TipoFuncionario) // inclui o tipo de funcionário
                    .FirstOrDefault(f => f.Email == loginDto.Email && f.Senha == loginDto.Password);

                if (funcionario == null)
                {
                    return Unauthorized("Email ou senha incorretos!");
                }

                // Pega o nome do tipo de funcionário (ex: Admin, Gestor, Usuário)
                string cargo = funcionario.TipoFuncionario?.TipoDeFuncionario ?? "Funcionario";


                // Cria a chave para o token
                var chave = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                // Cria as informações que vão dentro do token
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, funcionario.Nome),
                    new Claim(ClaimTypes.Email, funcionario.Email),
                    new Claim(ClaimTypes.Role, cargo)
                };

                // Configura o token
                var tokenConfig = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(chave),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };

                // Cria o token
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenConfig);

                // Retorna o token e as informações básicas
                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    nome = funcionario.Nome,
                    tipo = cargo
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao fazer login: " + ex.Message);
            }
        }
    }
}
