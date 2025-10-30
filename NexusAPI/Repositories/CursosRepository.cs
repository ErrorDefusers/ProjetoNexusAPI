using NexusAPI.Domains;
using NexusAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NexusAPI.Repositories
{
    public class CursosRepository : ICursosRepository
    {
        private readonly NexusContext _context;

        public CursosRepository(NexusContext context)
        {
            _context = context;
        }

        public void Salvar(Cursos curso)
        {
            try
            {
                _context.Cursos.Add(curso);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar curso: " + ex.Message);
            }
        }

        public void AtualizarCurso(Cursos curso)
        {
            try
            {
                _context.Cursos.Update(curso);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar curso: " + ex.Message);
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                var curso = _context.Cursos.Find(id);
                if (curso != null)
                {
                    _context.Cursos.Remove(curso);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar curso: " + ex.Message);
            }
        }

        
        public List<Cursos> Listar()
        {
            return _context.Cursos
                .Select(c => new Cursos
                {
                    IdCurso = c.IdCurso,
                    IdExterno = c.IdExterno ?? string.Empty,
                    Titulo = c.Titulo ?? string.Empty,
                    Descricao = c.Descricao ?? string.Empty,
                    Url = c.Url ?? string.Empty,
                    Progresso = c.Progresso ?? 0 
                })
                .ToList();
        }

        public void IdExterno(Guid id, string idExterno)
        {
            var curso = _context.Cursos.Find(id);
            if (curso != null)
            {
                curso.IdExterno = idExterno;
                _context.SaveChanges();
            }
        }

        public void Titulo(Guid id, string titulo)
        {
            var curso = _context.Cursos.Find(id);
            if (curso != null)
            {
                curso.Titulo = titulo;
                _context.SaveChanges();
            }
        }

        public void Url(Guid id, string url)
        {
            var curso = _context.Cursos.Find(id);
            if (curso != null)
            {
                curso.Url = url;
                _context.SaveChanges();
            }
        }

        public Cursos BuscarPorId(Guid id)
        {
            try
            {
                return _context.Cursos
                    .Select(e => new Cursos
                    {
                        IdCurso = e.IdCurso,
                        IdExterno = e.IdExterno ?? string.Empty,
                        Titulo = e.Titulo ?? string.Empty,
                        Descricao = e.Descricao ?? string.Empty,
                        Url = e.Url ?? string.Empty,
                        Progresso = e.Progresso ?? 0
                    })
                    .FirstOrDefault(e => e.IdCurso == id)!;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar curso: " + ex.Message);
            }
        }
    }
}
