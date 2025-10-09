
using Microsoft.EntityFrameworkCore;
using NexusAPI.Domains;
using NexusAPI.Interfaces;

namespace NexusAPI.Repositories
{
    public class FuncionariosCursosRepository : IFuncionariosCursosRepository
    {
        private readonly NexusContext _context;

        
        public FuncionariosCursosRepository(NexusContext context)
        {
            _context = context;
        }

        public void Salvar(Funcionarios funcionario)
        {
            try
            {
                
                var existente = _context.Funcionarios.Find(funcionario.IdFuncionario);

                if (existente == null)
                {
                    
                    _context.Funcionarios.Add(funcionario);
                }
                else
                {
                    
                    existente.Nome = funcionario.Nome;
                    existente.Email = funcionario.Email;
                    existente.Senha = funcionario.Senha;
                    existente.Cargo = funcionario.Cargo;

                    
                    existente.TipoFuncionarioId = funcionario.TipoFuncionarioId;
                    existente.SetorId = funcionario.SetorId;

                    existente.DataNascimento = funcionario.DataNascimento;

                    _context.Funcionarios.Update(existente);
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Deu ruim ao salvar funcionário: " + ex.Message);
            }
        }

        public List<Funcionarios> Listar()
        {
            try
            {
                
                return _context.Funcionarios
                    .Include(f => f.TipoFuncionario)
                    .Include(f => f.Setor)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Deu ruim ao listar funcionários: " + ex.Message);
            }
        }

        public Funcionarios BuscarPorEmail(string email)
        {
            try
            {
                
                return _context.Funcionarios
                    .Include(f => f.TipoFuncionario)
                    .Include(f => f.Setor)
                    .FirstOrDefault(f => f.Email.ToLower() == email.ToLower())!;
            }
            catch (Exception ex)
            {
                throw new Exception("Deu ruim ao buscar funcionário: " + ex.Message);
            }
        }
    }
}
