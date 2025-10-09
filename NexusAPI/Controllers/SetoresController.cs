using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;
using NexusAPI.Repositories;

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
                return Ok("Tipo de setor cadastrado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar setor: " + ex.Message);
            }
        }

    }
}