using NexusAPI.Domains;

namespace NexusAPI.Interfaces
{
    public interface IFuncionariosCursosRepository
    {
        void Salvar(Funcionarios funcionario); // adiciona ou atualiza
        List<Funcionarios> Listar(); // pega todos os funcionários
        Funcionarios BuscarPorEmail(string email); // busca por email
    }
}
