using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using NexusAPI.Interfaces;
using NexusAPI.DTO;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionariosRepository _funcRepository;

        public FuncionariosController(IFuncionariosRepository funcRepository)
        {
            _funcRepository = funcRepository;
        }

        
        [HttpPost("criar")]
        public IActionResult Criar(
            string nome,
            string email,
            string senha,
            DateTime dataNascimento,
            string cargo,
            Guid tipoFuncionarioId,
            Guid setorId,
            string role)
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

                _funcRepository.Salvar(funcionario);
                return Ok("Funcionário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar funcionário: " + ex.Message);
            }
        }

       
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
            string role)
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

                _funcRepository.Salvar(funcionario);
                return Ok("Funcionário atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar funcionário: " + ex.Message);
            }
        }

        
        [HttpPut("atualizar-imagem")]
        public IActionResult AtualizarImagem([FromForm] AtualizarImagemDTO dados)
        {
            if (dados.Imagem == null || dados.Id == Guid.Empty)
                return BadRequest("Arquivo ou ID inválido.");

            try
            {
                var funcionario = _funcRepository.Listar()
                    .FirstOrDefault(f => f.IdFuncionario == dados.Id);

                if (funcionario == null)
                    return NotFound("Funcionário não encontrado.");

                
                var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "funcionarios");
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                
                var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(dados.Imagem.FileName)}";
                var caminhoCompleto = Path.Combine(pasta, nomeArquivo);

                
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    dados.Imagem.CopyTo(stream);
                }

                
                funcionario.ImagemPerfil = $"/images/funcionarios/{nomeArquivo}";
                _funcRepository.Salvar(funcionario);

                return Ok(new { imagemPerfil = funcionario.ImagemPerfil });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar imagem: " + ex.Message);
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
                if (funcionario == null)
                    return NotFound("Funcionário não encontrado.");

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
                return Ok("Funcionário deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar funcionário: " + ex.Message);
            }
        }

        [HttpGet("setores")]
        public IActionResult ListarSetores()
        {
            try
            {
                var setores = _funcRepository.ListarSetores(); 
                return Ok(setores.Select(s => new { s.IdSetor, s.TipoSetor }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao listar setores: " + ex.Message);
            }
        }

        [HttpGet("cargos")]
        public IActionResult ListarCargos()
        {
            try
            {
                var cargos = _funcRepository.ListarTiposFuncionarios(); 
                return Ok(cargos.Select(c => new { c.IdTipoFuncionario, c.TipoDeFuncionario }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao listar cargos: " + ex.Message);
            }
        }

    }
}
