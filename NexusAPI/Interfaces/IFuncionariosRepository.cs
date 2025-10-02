using NexusAPI.Domains;

namespace NexusAPI.Interfaces
{
    public interface IFuncionariosRepository
    {
        void Salvar(Funcionarios funcionario);  // adiciona ou atualiza
        List<Funcionarios> Listar();  // lista todos os funcionarios
        Funcionarios BuscarPorEmail(string email);  // busca funcionario pelo email
    }
}
