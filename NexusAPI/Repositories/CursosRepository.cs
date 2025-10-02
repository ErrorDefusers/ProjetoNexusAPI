using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Repositories
{
    public class CursosRepository : ICursosRepository
    {
        private readonly NexusContext _context;


        public CursosRepository( NexusContext context)
        {
            _context = context;
        }


        public void IdExterno(Guid id, string idExterno)
        {
            try
           {
                var curso = _context.Cursos.Find(id);
                if (curso == null)
                    throw new Exception("Curso não encontrado.");
                curso.IdExterno = idExterno;
                _context.Cursos.Update(curso);
                _context.SaveChanges();
            }
           catch (Exception ex)
           {
                throw new Exception(ex.Message);
            }
        }

        public void Titulo(Guid id, string titulo)
        {
            try
            {
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Url(Guid id, string url)
        {
            throw new NotImplementedException();
        }
    }
}
