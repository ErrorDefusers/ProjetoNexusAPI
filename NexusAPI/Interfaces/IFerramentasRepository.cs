using NexusAPI.Domains;
using System;
using System.Collections.Generic;

namespace NexusAPI.Interfaces
{
    public interface IFerramentasRepository
    {
        void Salvar(Ferramentas ferramenta); // cria nova ferramenta
        void Atualizar(Ferramentas ferramenta); // atualiza existente
        void Deletar(Guid id); // deleta por id
        List<Ferramentas> Listar(); // lista todas
        Ferramentas BuscarPorId(Guid id); // busca por id
    }
}
