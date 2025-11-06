using NexusAPI.Domains;
using System;
using System.Collections.Generic;

namespace NexusAPI.Interfaces
{
    public interface IAcessosVideosRepository
    {
        void Registrar(AcessosVideos acesso);
        List<AcessosVideos> Listar();
        List<AcessosVideos> BuscarPorFuncionario(Guid funcionarioId);
        List<dynamic> Estatisticas();
    }
}
