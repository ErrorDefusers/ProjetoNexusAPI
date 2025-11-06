using NexusAPI.Domains;
using NexusAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NexusAPI.Repositories
{
    public class AcessosVideosRepository : IAcessosVideosRepository
    {
        private readonly NexusContext _context;

        public AcessosVideosRepository(NexusContext context)
        {
            _context = context;
        }

        public void Registrar(AcessosVideos acesso)
        {
            _context.AcessosVideos.Add(acesso);
            _context.SaveChanges();
        }

        public List<AcessosVideos> Listar()
        {
            return _context.AcessosVideos.ToList();
        }

        public List<AcessosVideos> BuscarPorFuncionario(Guid funcionarioId)
        {
            return _context.AcessosVideos
                .Where(a => a.FuncionarioId == funcionarioId)
                .OrderByDescending(a => a.DtAcesso)
                .ToList();
        }

        public List<dynamic> Estatisticas()
        {
            return _context.AcessosVideos
                .GroupBy(a => a.Curso.Titulo)
                .Select(g => new
                {
                    Curso = g.Key,
                    TotalAcessos = g.Count()
                })
                .ToList<dynamic>();
        }
    }
}
