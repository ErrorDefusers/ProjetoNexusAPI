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
            return _context.Cursos.ToList();
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
    }
}
