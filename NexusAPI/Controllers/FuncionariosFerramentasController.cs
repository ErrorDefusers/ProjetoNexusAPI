using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosFerramentasController : ControllerBase
    {
        private readonly IFuncionarioFerramentasRepository _repository;

        public FuncionariosFerramentasController(IFuncionarioFerramentasRepository repository)
        {
            _repository = repository;
        }

        
        [HttpPost("criar")]
        public IActionResult Criar(Guid funcionarioId, Guid ferramentaId)
        {
            try
            {
                var item = new FuncionarioFerramentas
                {
                    IdFuncionarioFerramenta = Guid.NewGuid(),
                    FuncionarioId = funcionarioId,
                    FerramentaId = ferramentaId
                };

                _repository.Salvar(item);
                return Ok("Vínculo criado/atualizado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar vínculo: " + ex.Message);
            }
        }

        
        [HttpGet("listar")]
        public IActionResult Listar()
        {
            try
            {
                var list = _repository.Listar();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao listar vínculos: " + ex.Message);
            }
        }

        
        [HttpGet("buscar/funcionario")]
        public IActionResult BuscarPorFuncionario(Guid funcionarioId)
        {
            try
            {
                var list = _repository.BuscarPorFuncionario(funcionarioId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar vínculos do funcionário: " + ex.Message);
            }
        }

        
        [HttpGet("buscar/ferramenta")]
        public IActionResult BuscarPorFerramenta(Guid ferramentaId)
        {
            try
            {
                var list = _repository.BuscarPorFerramenta(ferramentaId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar vínculos da ferramenta: " + ex.Message);
            }
        }
    }
}
