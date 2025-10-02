using NexusAPI.Domains;

namespace NexusAPI.Interfaces
{
    public interface IFuncionarioFerramentasRepository
    {
        void Salvar(FuncionarioFerramentas item); // adiciona ou atualiza
        List<FuncionarioFerramentas> Listar(); // lista todos
        List<FuncionarioFerramentas> BuscarPorFuncionario(Guid funcionarioId); // busca por funcionário
        List<FuncionarioFerramentas> BuscarPorFerramenta(Guid ferramentaId);   // busca por ferramenta
    }
}
