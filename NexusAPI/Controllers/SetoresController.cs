using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetoresController : ControllerBase
    {
        private readonly ISetoresRepository _setoresRepository;

        public SetoresController(ISetoresRepository setoresRepository)
        {
            _setoresRepository = setoresRepository;
        }

        
        [HttpPost("criar")]
        public IActionResult Criar(string tipoSetor)
        {
            try
            {
                Setores setor = new Setores();
                setor.TipoSetor = tipoSetor;

                _setoresRepository.Salvar(setor);

                return Ok("Setor cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar setor: " + ex.Message);
            }
        }

        
        [HttpGet("listar")]
        public IActionResult Listar()
        {
            var setores = _setoresRepository.Listar();
            return Ok(setores);
        }

        
        [HttpGet("buscar")]
        public IActionResult BuscarPorNome(string nome)
        {
            var setor = _setoresRepository.BuscarPorNome(nome);

            if (setor == null)
                return NotFound("Setor não encontrado.");

            return Ok(setor);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar(Guid id, string tipoSetor)
        {
            try
            {
                
                var setor = _setoresRepository.Listar().FirstOrDefault(s => s.IdSetor == id);

                if (setor == null)
                    return NotFound("Setor não encontrado!");

                
                setor.TipoSetor = tipoSetor;

                
                _setoresRepository.Salvar(setor);

                return Ok("Setor atualizado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar setor: " + ex.Message);
            }
        }

    }
}
