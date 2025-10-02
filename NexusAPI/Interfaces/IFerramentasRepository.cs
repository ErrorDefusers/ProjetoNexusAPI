using NexusAPI.Domains;

namespace NexusAPI.Interfaces
{
    public interface IFerramentasRepository
    {
        void Salvar(Guid id, Ferramentas ferramenta);  // adiciona ou atualiza
        List<Ferramentas> Listar();  // pega todos as ferramentas
        Ferramentas BuscarPorNome(Guid id, string nome); // busca por nome
    }
}
