using NexusAPI.Domains;
using NexusAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NexusAPI.Repositories
{
    public class TiposFuncionariosRepository : ITiposFuncionariosRepository
    {
        private readonly NexusContext _context;

        public TiposFuncionariosRepository(NexusContext context)
        {
            _context = context;
        }

        
        public void Salvar(TiposFuncionarios tipoFuncionario)
        {
            var existente = _context.TiposFuncionarios
                .FirstOrDefault(t => t.IdTipoFuncionario == tipoFuncionario.IdTipoFuncionario);

            if (existente == null)
            {
                _context.TiposFuncionarios.Add(tipoFuncionario);
            }
            else
            {
                existente.TipoDeFuncionario = tipoFuncionario.TipoDeFuncionario;
                _context.TiposFuncionarios.Update(existente);
            }

            _context.SaveChanges();
        }

        
        public List<TiposFuncionarios> Listar()
        {
            return _context.TiposFuncionarios.ToList();
        }

        
        public TiposFuncionarios? BuscarPorTipo(string tipo)
        {
            return _context.TiposFuncionarios.FirstOrDefault(t => t.TipoDeFuncionario == tipo);
        }

        
        public void Deletar(Guid id)
        {
            var tipo = _context.TiposFuncionarios.FirstOrDefault(t => t.IdTipoFuncionario == id);
            if (tipo != null)
            {
                _context.TiposFuncionarios.Remove(tipo);
                _context.SaveChanges();
            }
        }
    }
}
