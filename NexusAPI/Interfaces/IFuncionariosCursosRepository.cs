using NexusAPI.Domains;
using System;
using System.Collections.Generic;

namespace NexusAPI.Interfaces
{
    public interface IFuncionariosCursosRepository
    {
        void Salvar(FuncionariosCursos item);
        List<FuncionariosCursos> Listar();
        List<FuncionariosCursos> BuscarPorFuncionario(Guid funcionarioId);
        List<FuncionariosCursos> BuscarPorCurso(Guid cursoId);
        void Deletar(Guid id);
    }
}
