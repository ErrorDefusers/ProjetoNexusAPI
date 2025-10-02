using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Repositories
{
    public class FerramentasRepository : IFerramentasRepository
    {
        private readonly NexusContext _context;

        public FerramentasRepository(NexusContext context)
        {
            _context = context;
        }

        public Ferramentas BuscarPorNome(Guid id, string nome)
        {
            try
            {
                return _context.Ferramentas.Select(e => new Ferramentas
                {
                    Nome = e.Nome,
                    Url = e.Url

                }).FirstOrDefault(e => e.Nome == nome);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Ferramentas> Listar()
        {
            try
            {
                return _context.Ferramentas.Select(e => new Ferramentas
                {
                    Nome = e.Nome,
                    Url = e.Url

                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Salvar(Guid id, Ferramentas ferramenta)
        {
            try
            {
                var ferramentaExistente = _context.Ferramentas.Find(ferramenta.IdFerramenta);
                if (ferramentaExistente == null)
                {
                    _context.Ferramentas.Add(ferramenta);
                }
                else
                {
                    ferramentaExistente.Nome = ferramenta.Nome;
                    ferramentaExistente.Url = ferramenta.Url;
                    _context.Ferramentas.Update(ferramentaExistente);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
