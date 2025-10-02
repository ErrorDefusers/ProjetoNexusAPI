using NexusAPI.Domains;

namespace NexusAPI.Interfaces
{
    public interface IFerramentasRepository
    {
        void Salvar(Ferramentas ferramenta);  // adiciona ou atualiza
        List<Ferramentas> Listar();  // pega todos as ferramentas
        Ferramentas BuscarPorNome(string nome); // busca por nome
    }
}
