using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposFuncionariosController : ControllerBase
    {
        private readonly ITiposFuncionariosRepository _tiposRepository;

        public TiposFuncionariosController(ITiposFuncionariosRepository tiposRepository)
        {
            _tiposRepository = tiposRepository;
        }

        
        [HttpPost("criar")]
        public IActionResult Criar(string tipoFuncionario)
        {
            try
            {
                TiposFuncionarios tipo = new TiposFuncionarios
                {
                    IdTipoFuncionario = Guid.NewGuid(),
                    TipoDeFuncionario = tipoFuncionario
                };

                _tiposRepository.Salvar(tipo);
                return Ok("Tipo de funcionário cadastrado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar tipo de funcionário: " + ex.Message);
            }
        }

        
        [HttpGet("listar")]
        public IActionResult Listar()
        {
            var tipos = _tiposRepository.Listar();
            return Ok(tipos);
        }

        
        [HttpGet("buscar")]
        public IActionResult Buscar(string tipo)
        {
            var resultado = _tiposRepository.BuscarPorTipo(tipo);
            if (resultado == null) return NotFound("Tipo não encontrado");
            return Ok(resultado);
        }

        
        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar(Guid id, string tipoFuncionario)
        {
            try
            {
                TiposFuncionarios tipo = new TiposFuncionarios
                {
                    IdTipoFuncionario = id,
                    TipoDeFuncionario = tipoFuncionario
                };

                _tiposRepository.Salvar(tipo); 
                return Ok("Tipo de funcionário atualizado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar tipo de funcionário: " + ex.Message);
            }
        }

        
        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _tiposRepository.Deletar(id);
                return Ok("Tipo de funcionário deletado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar tipo de funcionário: " + ex.Message);
            }
        }
    }
}
