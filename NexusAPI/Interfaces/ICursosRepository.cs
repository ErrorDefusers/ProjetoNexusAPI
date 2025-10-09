using NexusAPI.Domains;
using NexusAPI.Repositories;

namespace NexusAPI.Interfaces
{
    public interface ICursosRepository
    {
        void IdExterno(Guid id, string idExterno); // adiciona ou atualiza

        void Titulo(Guid id, string titulo); // adiciona ou atualiza

        void Url(Guid id, string url); // adiciona ou atualiza
        List<Cursos> Listar(); // Listar os cursos
        void Salvar(Cursos curso); // Salva os cursos
        void AtualizarCurso(Cursos curso); // Atualiza
        void Deletar(Guid id);


    }
}
