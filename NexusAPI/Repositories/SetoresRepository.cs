using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Repositories
{
    public class SetoresRepository : ISetoresRepository
    {
        private readonly NexusContext _context;

        public SetoresRepository(NexusContext context)
        {
            _context = context;
        }

        
        public void Salvar(Setores setor)
        {
            _context.Setores.Add(setor);
            _context.SaveChanges();
        }

        
        public List<Setores> Listar()
        {
            return _context.Setores.ToList();
        }

        
        public Setores? BuscarPorNome(string nome)
        {
            return _context.Setores.FirstOrDefault(s => s.TipoSetor == nome);
        }

        
        public void Deletar(Guid id)
        {
            var setor = _context.Setores.Find(id);
            if (setor != null)
            {
                _context.Setores.Remove(setor);
                _context.SaveChanges();
            }
        }
    }
}
