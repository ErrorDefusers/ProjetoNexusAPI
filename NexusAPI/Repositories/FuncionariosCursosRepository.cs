using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Repositories
{
    public class FuncionariosCursosRepository : IFuncionarioFerramentasRepository
    {

        private readonly NexusContext _context;


        public FuncionariosCursosRepository(NexusContext context)
        {
            _context = context;
        }

        public List<FuncionarioFerramentas> BuscarPorFerramenta(Guid ferramentaId)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                
            }
        }

        public List<FuncionarioFerramentas> BuscarPorFuncionario(Guid funcionarioId)
        {
            throw new NotImplementedException();
        }

        public List<FuncionarioFerramentas> Listar()
        {
            throw new NotImplementedException();
        }

        public void Salvar(FuncionarioFerramentas item)
        {
            throw new NotImplementedException();
        }
    }
}
