using NexusAPI.Domains;
using NexusAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NexusAPI.Repositories
{
    public class FuncionariosRepository : IFuncionariosRepository
    {
        private readonly NexusContext _context;

        public FuncionariosRepository(NexusContext context)
        {
            _context = context;
        }

        public void Salvar(Funcionarios funcionario)
        {
            var existente = _context.Funcionarios.Find(funcionario.IdFuncionario);

            if (existente == null)
                _context.Funcionarios.Add(funcionario);
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

        public List<Funcionarios> Listar()
        {
            return _context.Funcionarios
                .Include(f => f.TipoFuncionario)
                .Include(f => f.Setor)
                .ToList();
        }

        public Funcionarios? BuscarPorEmail(string email)
        {
            return _context.Funcionarios
                .Include(f => f.TipoFuncionario)
                .Include(f => f.Setor)
                .FirstOrDefault(f => f.Email.ToLower() == email.ToLower());
        }

        public void Deletar(Guid id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                _context.SaveChanges();
            }
        }
    }
}
