using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;
using System;
using System.IO;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionariosRepository _funcRepository;
        private readonly IWebHostEnvironment _environment;

        public FuncionariosController(IFuncionariosRepository funcRepository, IWebHostEnvironment environment)
        {
            _funcRepository = funcRepository;
            _environment = environment;
        }

        // Criar funcionário 
        [HttpPost("criar")]
        public IActionResult Criar(
            string nome,
            string email,
            string senha,
            DateTime dataNascimento,
            string cargo,
            Guid tipoFuncionarioId,
            Guid setorId,
            string role,
            IFormFile? imagem = null)
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
                    SetorId = setorId,
                    Role = role
                };

                // Salvar imagem, se houver
                if (imagem != null && imagem.Length > 0)
                {
                    
                    var webRoot = _environment.WebRootPath;
                    if (string.IsNullOrEmpty(webRoot))
                        webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                    string pastaImagens = Path.Combine(webRoot, "imagensPerfil");
                    if (!Directory.Exists(pastaImagens))
                        Directory.CreateDirectory(pastaImagens);

                    string nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(imagem.FileName)}";
                    string caminhoCompleto = Path.Combine(pastaImagens, nomeArquivo);

                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }

                    funcionario.ImagemPerfil = $"/imagensPerfil/{nomeArquivo}";
                }

                _funcRepository.Salvar(funcionario);
                return Ok("Funcionário cadastrado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar funcionário: " + ex.Message);
            }
        }

        // Atualizar funcionário 
        [HttpPut("atualizar")]
        public IActionResult Atualizar(
            Guid id,
            string nome,
            string email,
            string senha,
            DateTime dataNascimento,
            string cargo,
            Guid tipoFuncionarioId,
            Guid setorId,
            string role,
            IFormFile? imagem = null)
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
                    SetorId = setorId,
                    Role = role
                };

                if (imagem != null && imagem.Length > 0)
                {
                    var webRoot = _environment.WebRootPath;
                    if (string.IsNullOrEmpty(webRoot))
                        webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                    string pastaImagens = Path.Combine(webRoot, "imagensPerfil");
                    if (!Directory.Exists(pastaImagens))
                        Directory.CreateDirectory(pastaImagens);

                    string nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(imagem.FileName)}";
                    string caminhoCompleto = Path.Combine(pastaImagens, nomeArquivo);

                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }

                    funcionario.ImagemPerfil = $"/imagensPerfil/{nomeArquivo}";
                }

                _funcRepository.Salvar(funcionario);
                return Ok("Funcionário atualizado!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar funcionário: " + ex.Message);
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
