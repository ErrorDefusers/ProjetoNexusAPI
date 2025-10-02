namespace NexusAPI.Interfaces
{
    public interface ICursosRepository
    {
        void IdExterno(Guid id, string idExterno); // adiciona ou atualiza

        void Titulo(Guid id, string titulo); // adiciona ou atualiza

        void Url(Guid id, string url); // adiciona ou atualiza
    }
}
