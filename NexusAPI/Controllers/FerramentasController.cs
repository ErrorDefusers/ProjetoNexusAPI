using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;
using System;
using System.Collections.Generic;

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

        [HttpGet]
        public IActionResult Listar()
        {
            var ferramentas = _ferramentasRepository.Listar();
            return Ok(ferramentas);
        }

        
        [HttpPost("criar")]
        public IActionResult Criar(string nome, string url, string tipo, bool status = true)
        {
            try
            {
                Ferramentas ferramenta = new Ferramentas();
                ferramenta.IdFerramenta = Guid.NewGuid();
                ferramenta.Nome = nome;
                ferramenta.Url = url;
                ferramenta.Tipo = tipo;
                ferramenta.Status = status;

                _ferramentasRepository.Salvar(ferramenta);
                return Ok("Ferramenta criada!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar ferramenta: " + ex.Message);
            }
        }

        
        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar(Guid id, string nome, string url, string tipo, bool status = true)
        {
            try
            {
                Ferramentas ferramenta = new Ferramentas();
                ferramenta.IdFerramenta = id;
                ferramenta.Nome = nome;
                ferramenta.Url = url;
                ferramenta.Tipo = tipo;
                ferramenta.Status = status;

                _ferramentasRepository.Atualizar(ferramenta);
                return Ok("Ferramenta atualizada!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar ferramenta: " + ex.Message);
            }
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _ferramentasRepository.Deletar(id);
                return Ok("Ferramenta deletada!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar ferramenta: " + ex.Message);
            }
        }

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
