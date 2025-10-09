using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NexusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly ICursosRepository _cursosRepository;

        public CursosController(ICursosRepository cursosRepository)
        {
            _cursosRepository = cursosRepository;
        }

        
        [HttpGet]
        public IActionResult Listar()
        {
            var cursos = _cursosRepository.Listar();
            return Ok(cursos);
        }

        
        [HttpPost]
        public IActionResult Criar(Cursos curso)
        {
            try
            {
                curso.IdCurso = Guid.NewGuid(); 
                _cursosRepository.Salvar(curso);
                return Ok("Curso criado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar curso: " + ex.Message);
            }
        }

        
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, Cursos curso)
        {
            try
            {
                curso.IdCurso = id; 
                _cursosRepository.AtualizarCurso(curso);
                return Ok("Curso atualizado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar curso: " + ex.Message);
            }
        }

        
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _cursosRepository.Deletar(id);
                return Ok("Curso deletado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar curso: " + ex.Message);
            }
        }

        
        [HttpGet("externos")]
        public IActionResult ListarExternos()
        {
            var cursosExternos = new List<Cursos>
            {
                new Cursos { IdCurso = Guid.NewGuid(), Titulo = "Fundamentos de TI", Url = "https://cursa.app/curso1", IdExterno = "C001", Descricao = "Curso básico de fundamentos de TI" },
                new Cursos { IdCurso = Guid.NewGuid(), Titulo = "Introdução à Programação", Url = "https://cursa.app/curso2", IdExterno = "C002", Descricao = "Aprenda lógica de programação do zero" },
                new Cursos { IdCurso = Guid.NewGuid(), Titulo = "Segurança da Informação Básica", Url = "https://cursa.app/curso3", IdExterno = "C003", Descricao = "Noções básicas de segurança em TI" },
                new Cursos { IdCurso = Guid.NewGuid(), Titulo = "Desenvolvimento Web com HTML/CSS", Url = "https://cursa.app/curso4", IdExterno = "C004", Descricao = "Aprenda a criar páginas web" }
            };

            return Ok(cursosExternos);
        }
    }
}
