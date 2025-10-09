using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosCursosController : ControllerBase
    {
        private readonly IFuncionariosCursosRepository _repository;

        public FuncionariosCursosController(IFuncionariosCursosRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("criar")]
        public IActionResult Criar([FromBody] FuncionariosCursos item)
        {
            try
            {
                item.IdFuncionarioCurso = Guid.NewGuid();
                _repository.Salvar(item);
                return Ok("Relação Funcionário-Curso criada!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar: {ex.Message}");
            }
        }

        [HttpGet("listar")]
        public IActionResult Listar()
        {
            try
            {
                var lista = _repository.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar: {ex.Message}");
            }
        }

        [HttpGet("buscar/funcionario/{funcionarioId}")]
        public IActionResult BuscarPorFuncionario(Guid funcionarioId)
        {
            try
            {
                var lista = _repository.BuscarPorFuncionario(funcionarioId);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar: {ex.Message}");
            }
        }

        [HttpGet("buscar/curso/{cursoId}")]
        public IActionResult BuscarPorCurso(Guid cursoId)
        {
            try
            {
                var lista = _repository.BuscarPorCurso(cursoId);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar: {ex.Message}");
            }
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _repository.Deletar(id);
                return Ok("Relação Funcionário-Curso deletada!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar: {ex.Message}");
            }
        }
    }
}
