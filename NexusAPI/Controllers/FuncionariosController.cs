using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionariosRepository _funcRepository;

        public FuncionariosController(IFuncionariosRepository funcRepository)
        {
            _funcRepository = funcRepository;
        }

        
        [HttpPost("criar")]
        public IActionResult Criar(string nome, string email, string senha, DateTime dataNascimento, string cargo, Guid tipoFuncionarioId, Guid setorId)
        {
            try
            {
                Funcionarios funcionario = new()
                {
                    IdFuncionario = Guid.NewGuid(),
                    Nome = nome,
                    Email = email,
                    Senha = senha,
                    DataNascimento = dataNascimento,
                    Cargo = cargo,
                    TipoFuncionarioId = tipoFuncionarioId,
                    SetorId = setorId
                };

                _funcRepository.Salvar(funcionario);
                return Ok("Funcionário cadastrado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar funcionário: " + ex.Message);
            }
        }

        
        [HttpGet("listar")]
        public IActionResult Listar()
        {
            try
            {
                var funcionarios = _funcRepository.Listar();
                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao listar funcionários: " + ex.Message);
            }
        }

        
        [HttpGet("buscar")]
        public IActionResult Buscar(string email)
        {
            try
            {
                var funcionario = _funcRepository.BuscarPorEmail(email);
                if (funcionario == null) return NotFound("Funcionário não encontrado");
                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar funcionário: " + ex.Message);
            }
        }

        
        [HttpPut("atualizar")]
        public IActionResult Atualizar(Guid id, string nome, string email, string senha, DateTime dataNascimento, string cargo, Guid tipoFuncionarioId, Guid setorId)
        {
            try
            {
                Funcionarios funcionario = new()
                {
                    IdFuncionario = id,
                    Nome = nome,
                    Email = email,
                    Senha = senha,
                    DataNascimento = dataNascimento,
                    Cargo = cargo,
                    TipoFuncionarioId = tipoFuncionarioId,
                    SetorId = setorId
                };

                _funcRepository.Salvar(funcionario); 
                return Ok("Funcionário atualizado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar funcionário: " + ex.Message);
            }
        }

        
        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _funcRepository.Deletar(id);
                return Ok("Funcionário deletado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar funcionário: " + ex.Message);
            }
        }
    }
}
