using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;
using System;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessosVideosController : ControllerBase
    {
        private readonly IAcessosVideosRepository _acessosVideosRepository;

        public AcessosVideosController(IAcessosVideosRepository acessosVideosRepository)
        {
            _acessosVideosRepository = acessosVideosRepository;
        }

        [HttpPost("registrar")]
        public IActionResult Registrar(AcessosVideos acesso)
        {
            try
            {
                _acessosVideosRepository.Registrar(acesso);
                return Ok("Acesso de vídeo registrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao registrar acesso: " + ex.Message);
            }
        }

        [HttpGet("funcionario/{id}")]
        public IActionResult BuscarPorFuncionario(Guid id)
        {
            try
            {
                var acessos = _acessosVideosRepository.BuscarPorFuncionario(id);
                return Ok(acessos);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar acessos: " + ex.Message);
            }
        }

        [HttpGet("estatisticas")]
        public IActionResult Estatisticas()
        {
            try
            {
                var stats = _acessosVideosRepository.Estatisticas();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao obter estatísticas: " + ex.Message);
            }
        }
    }
}
