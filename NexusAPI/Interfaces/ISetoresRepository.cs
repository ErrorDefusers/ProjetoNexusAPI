using NexusAPI.Domains;
namespace NexusAPI.Interfaces
{
    public interface ISetoresRepository
    {
        void Salvar(Setores setor);  // adiciona ou atualiza
        List<Setores> Listar();  // lista todos os setores
        Setores BuscarPorNome(string nome); // busca setor pelo nome
    }
}