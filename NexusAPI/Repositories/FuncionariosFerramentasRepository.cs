using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Repositories
{
    public class FuncionariosFerramentasRepository : IFuncionarioFerramentasRepository
    {
        public List<FuncionarioFerramentas> BuscarPorFerramenta(Guid ferramentaId)
        {
            throw new NotImplementedException();
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
