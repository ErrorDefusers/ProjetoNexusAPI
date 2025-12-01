using NexusAPI.Domains;

namespace NexusAPI.Interfaces
{
    public interface ITiposFuncionariosRepository
    {
        void Salvar(TiposFuncionarios tipoFuncionario);  // adiciona ou atualiza
        List<TiposFuncionarios> Listar(); // lista todos os tipos
        TiposFuncionarios BuscarPorTipo(string tipo); // busca por tipo
        void Deletar(Guid id);
    }
}
