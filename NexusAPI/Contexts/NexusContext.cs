using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Domains
{
    public class NexusContext : DbContext
    {
       public NexusContext() { }
        public NexusContext(DbContextOptions<NexusContext> options) : base(options) { }

        
        public DbSet<Cursos> Cursos { get; set; }
        public DbSet<Ferramentas> Ferramentas { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<FuncionariosCursos> FuncionariosCursos { get; set; }
        public DbSet<Setores> Setores { get; set; }
        public DbSet<TiposFuncionarios> TiposFuncionarios { get; set; }
        public DbSet<FuncionarioFerramentas> FuncionariosFerramentas { get; set; }  



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=NOTE22-S28\\SQLEXPRESS; Database=nexus; User Id=sa; Pwd=Senai@134; TrustServerCertificate=true;"
                );
            }
        }
    }
}
