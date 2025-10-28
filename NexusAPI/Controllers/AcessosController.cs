using Microsoft.AspNetCore.Mvc;
using NexusAPI.Domains;
using System;
using System.Linq;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class AcessosController : ControllerBase
{
    private readonly NexusContext _context;

    public AcessosController(NexusContext context)
    {
        _context = context;
    }

    [HttpPost("registrar")]
    public IActionResult RegistrarAcesso(string funcionarioId, string ferramentaId)
    {
        if (string.IsNullOrEmpty(funcionarioId) || string.IsNullOrEmpty(ferramentaId))
            return BadRequest("Informe o ID do funcionario e da ferramenta.");

        DtAcesso acesso = new DtAcesso();
        acesso.IdDtAcesso = Guid.NewGuid();
        acesso.IdFuncionario = Guid.Parse(funcionarioId);
        acesso.IdFerramenta = Guid.Parse(ferramentaId);
        acesso.DataAcesso = DateTime.Now;

        _context.DtAcessos.Add(acesso);
        _context.SaveChanges();

        return Ok("Acesso registrado com sucesso!");
    }

    [HttpGet("funcionario/{id}")]
    public IActionResult ObterAcessosFuncionario(string id)
    {
        Guid idFuncionario = Guid.Parse(id);
        var acessos = _context.DtAcessos
            .Where(a => a.IdFuncionario == idFuncionario)
            .ToList();

        if (acessos.Count == 0)
            return NotFound("Nenhum acesso encontrado.");

        return Ok(acessos);
    }

    [HttpGet("estatisticas")]
    public IActionResult ObterEstatisticas()
    {
        var dias = _context.DtAcessos
            .Select(a => a.DataAcesso.Date)
            .Distinct()
            .OrderBy(d => d)
            .ToList();

        var ferramentas = _context.Ferramentas.ToList();
        var resultado = new List<object>();

        foreach (var ferramenta in ferramentas)
        {
            var acessosPorDia = dias.Select(d =>
                _context.DtAcessos.Count(a => a.IdFerramenta == ferramenta.IdFerramenta && a.DataAcesso.Date == d)
            ).ToList();

            resultado.Add(new
            {
                nomeFerramenta = ferramenta.Nome, 
                dias = dias.Select(d => d.ToString("dd MMM")).ToList(),
                acessos = acessosPorDia
            });
        }

        return Ok(resultado);
    }

}
