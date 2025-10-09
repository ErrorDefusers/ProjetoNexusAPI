using NexusAPI.Domains;
using NexusAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NexusAPI.Repositories
{
    public class FuncionariosCursosRepository : IFuncionariosCursosRepository
    {
        private readonly NexusContext _context;

        public FuncionariosCursosRepository(NexusContext context)
        {
            _context = context;
        }

        public void Salvar(FuncionariosCursos item)
        {
            var existente = _context.FuncionariosCursos
                .FirstOrDefault(f => f.IdFuncionarioCurso == item.IdFuncionarioCurso);

            if (existente == null)
                _context.FuncionariosCursos.Add(item);
            else
            {
                existente.CursoId = item.CursoId;
                existente.FuncionarioId = item.FuncionarioId;
                _context.FuncionariosCursos.Update(existente);
            }

            _context.SaveChanges();
        }

        public List<FuncionariosCursos> Listar()
        {
            return _context.FuncionariosCursos
                .Include(f => f.Funcionario)
                .Include(f => f.Curso)
                .ToList();
        }

        public List<FuncionariosCursos> BuscarPorFuncionario(Guid funcionarioId)
        {
            return _context.FuncionariosCursos
                .Include(f => f.Funcionario)
                .Include(f => f.Curso)
                .Where(f => f.FuncionarioId == funcionarioId)
                .ToList();
        }

        public List<FuncionariosCursos> BuscarPorCurso(Guid cursoId)
        {
            return _context.FuncionariosCursos
                .Include(f => f.Funcionario)
                .Include(f => f.Curso)
                .Where(f => f.CursoId == cursoId)
                .ToList();
        }

        public void Deletar(Guid id)
        {
            var item = _context.FuncionariosCursos.Find(id);
            if (item != null)
            {
                _context.FuncionariosCursos.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
