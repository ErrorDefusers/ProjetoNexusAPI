using Microsoft.EntityFrameworkCore;
using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Repositories
{
    public class FuncionariosFerramentasRepository : IFuncionarioFerramentasRepository
    {
        private readonly NexusContext _context;

        public FuncionariosFerramentasRepository(NexusContext context)
        {
            _context = context;
        }

        
        public void Salvar(FuncionarioFerramentas item)
        {
            var existente = _context.FuncionariosFerramentas
                .FirstOrDefault(f => f.IdFuncionarioFerramenta == item.IdFuncionarioFerramenta);

            if (existente == null)
            {
                _context.FuncionariosFerramentas.Add(item);
            }
            else
            {
                existente.FuncionarioId = item.FuncionarioId;
                existente.FerramentaId = item.FerramentaId;
                _context.FuncionariosFerramentas.Update(existente);
            }

            _context.SaveChanges();
        }

        
        public List<FuncionarioFerramentas> Listar()
        {
            return _context.FuncionariosFerramentas
                .Include(f => f.Funcionario)
                .Include(f => f.Ferramenta)
                .ToList();
        }

        
        public List<FuncionarioFerramentas> BuscarPorFuncionario(Guid funcionarioId)
        {
            return _context.FuncionariosFerramentas
                .Include(f => f.Funcionario)
                .Include(f => f.Ferramenta)
                .Where(f => f.FuncionarioId == funcionarioId)
                .ToList();
        }

        
        public List<FuncionarioFerramentas> BuscarPorFerramenta(Guid ferramentaId)
        {
            return _context.FuncionariosFerramentas
                .Include(f => f.Funcionario)
                .Include(f => f.Ferramenta)
                .Where(f => f.FerramentaId == ferramentaId)
                .ToList();
        }
    }
}
