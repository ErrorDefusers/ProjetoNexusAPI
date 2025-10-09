using NexusAPI.Domains;
using System;
using System.Collections.Generic;

namespace NexusAPI.Interfaces
{
    public interface IFuncionariosRepository
    {
        void Salvar(Funcionarios funcionario);
        List<Funcionarios> Listar();
        Funcionarios? BuscarPorEmail(string email);
        void Deletar(Guid id);
    }
}
