using System;
using System.Collections.Generic;
using System.Linq;
using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Repositories
{
    public class FerramentasRepository : IFerramentasRepository
    {
        private readonly NexusContext _context;

        public FerramentasRepository(NexusContext context)
        {
            _context = context;
        }

        // Criar nova ferramenta
        public void Salvar(Ferramentas ferramenta)
        {
            try
            {
                _context.Ferramentas.Add(ferramenta);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar ferramenta: " + ex.Message);
            }
        }

        // Atualizar ferramenta existente
        public void Atualizar(Ferramentas ferramenta)
        {
            try
            {
                var ferramentaExistente = _context.Ferramentas.Find(ferramenta.IdFerramenta);

                if (ferramentaExistente == null)
                    throw new Exception("Ferramenta não encontrada.");

                ferramentaExistente.Nome = ferramenta.Nome;
                ferramentaExistente.Url = ferramenta.Url;
                ferramentaExistente.Descricao = ferramenta.Descricao;
                ferramentaExistente.Tipo = ferramenta.Tipo;
                ferramentaExistente.Status = ferramenta.Status;

                _context.Ferramentas.Update(ferramentaExistente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar ferramenta: " + ex.Message);
            }
        }

        // Deletar por ID
        public void Deletar(Guid id)
        {
            try
            {
                var ferramenta = _context.Ferramentas.Find(id);

                if (ferramenta == null)
                    throw new Exception("Ferramenta não encontrada.");

                _context.Ferramentas.Remove(ferramenta);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar ferramenta: " + ex.Message);
            }
        }

        // Listar todas
        public List<Ferramentas> Listar()
        {
            return _context.Ferramentas.ToList();
        }

        // Buscar por ID
        public Ferramentas BuscarPorId(Guid id)
        {
            return _context.Ferramentas.FirstOrDefault(f => f.IdFerramenta == id);
        }
    }
}
