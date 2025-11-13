using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FerramentasController : ControllerBase
    {
        private readonly IFerramentasRepository _ferramentasRepository;

        public FerramentasController(IFerramentasRepository ferramentasRepository)
        {
            _ferramentasRepository = ferramentasRepository;
        }

        // 🔹 GET: api/Ferramentas
        [HttpGet]
        public IActionResult Listar()
        {
            var ferramentas = _ferramentasRepository.Listar();
            return Ok(ferramentas);
        }

        // 🔹 GET: api/Ferramentas/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            var ferramenta = _ferramentasRepository.BuscarPorId(id);

            if (ferramenta == null)
                return NotFound("Ferramenta não encontrada.");

            return Ok(ferramenta);
        }

        // 🔹 POST: api/Ferramentas
        [HttpPost]
        public IActionResult Criar([FromBody] Ferramentas ferramenta)
        {
            try
            {
                ferramenta.IdFerramenta = Guid.NewGuid();
                ferramenta.Status = ferramenta.Status; // mantém o valor recebido (true/false)

                _ferramentasRepository.Salvar(ferramenta);
                return CreatedAtAction(nameof(BuscarPorId), new { id = ferramenta.IdFerramenta }, ferramenta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar ferramenta: {ex.Message}");
            }
        }

        // 🔹 PUT: api/Ferramentas/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] Ferramentas ferramenta)
        {
            try
            {
                var ferramentaExistente = _ferramentasRepository.BuscarPorId(id);

                if (ferramentaExistente == null)
                    return NotFound("Ferramenta não encontrada.");

                ferramentaExistente.Nome = ferramenta.Nome;
                ferramentaExistente.Url = ferramenta.Url;
                ferramentaExistente.Tipo = ferramenta.Tipo;
                ferramentaExistente.Status = ferramenta.Status;

                _ferramentasRepository.Atualizar(ferramentaExistente);

                return Ok("Ferramenta atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar ferramenta: {ex.Message}");
            }
        }

        // 🔹 DELETE: api/Ferramentas/{id}
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                var ferramenta = _ferramentasRepository.BuscarPorId(id);

                if (ferramenta == null)
                    return NotFound("Ferramenta não encontrada.");

                _ferramentasRepository.Deletar(id);
                return Ok("Ferramenta deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar ferramenta: {ex.Message}");
            }
        }

        // 🔹 GET: api/Ferramentas/externos
        [HttpGet("externos")]
        public IActionResult ListarExternos()
        {
            var ferramentasExternas = new List<Ferramentas>
            {
                new Ferramentas { IdFerramenta = Guid.NewGuid(), Nome = "Google Drive", Url = "https://drive.google.com", Tipo = "Armazenamento", Status = true },
                new Ferramentas { IdFerramenta = Guid.NewGuid(), Nome = "Google Docs", Url = "https://docs.google.com", Tipo = "Editor de Texto", Status = true },
                new Ferramentas { IdFerramenta = Guid.NewGuid(), Nome = "Google Meet", Url = "https://meet.google.com", Tipo = "Videoconferência", Status = true },
                new Ferramentas { IdFerramenta = Guid.NewGuid(), Nome = "Google Forms", Url = "https://forms.google.com", Tipo = "Formulários", Status = true },
                new Ferramentas { IdFerramenta = Guid.NewGuid(), Nome = "Gmail", Url = "https://mail.google.com", Tipo = "Email", Status = true }
            };

            return Ok(ferramentasExternas);
        }
    }
}
