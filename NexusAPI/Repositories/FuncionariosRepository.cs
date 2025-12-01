using NexusAPI.Domains;
using NexusAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Repositories
{
    public class FuncionariosRepository : IFuncionariosRepository
    {
        private readonly NexusContext _context;

        public FuncionariosRepository(NexusContext context)
        {
            _context = context;
        }

        // Salvar ou atualizar funcionário
        public void Salvar(Funcionarios funcionario)
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
                existente.Role = funcionario.Role;

                // Atualiza a imagem caso exista
                if (!string.IsNullOrEmpty(funcionario.ImagemPerfil))
                    existente.ImagemPerfil = funcionario.ImagemPerfil;

                _context.Funcionarios.Update(existente);
            }

            _context.SaveChanges();
        }

        // Listar todos os funcionários
        public List<Funcionarios> Listar()
        {
            return _context.Funcionarios
                .Include(f => f.TipoFuncionario)
                .Include(f => f.Setor)
                .ToList();
        }

        // Buscar funcionário por email
        public Funcionarios? BuscarPorEmail(string email)
        {
            return _context.Funcionarios
                .Include(f => f.TipoFuncionario)
                .Include(f => f.Setor)
                .FirstOrDefault(f => f.Email.ToLower() == email.ToLower());
        }

        // Deletar funcionário
        public void Deletar(Guid id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                _context.SaveChanges();
            }
        }

        // Atualizar apenas a imagem do funcionário
        public void AtualizarImagem(Guid idFuncionario, string caminhoImagem)
        {
            var funcionario = _context.Funcionarios.Find(idFuncionario);
            if (funcionario != null)
            {
                funcionario.ImagemPerfil = caminhoImagem;
                _context.SaveChanges();
            }
        }

        // Listar todos os setores
        public List<Setores> ListarSetores()
        {
            return _context.Setores.ToList();
        }

        // Listar todos os tipos de funcionários (cargos)
        public List<TiposFuncionarios> ListarTiposFuncionarios()
        {
            return _context.TiposFuncionarios.ToList();
        }

    }
}
