using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusAPI.Domains
{
    [Table("Acessos")]
    public class Acessos
    {
        [Key]
        public Guid IdAcesso { get; set; } = Guid.NewGuid();

        [Required]
        public Guid FuncionarioId { get; set; }

        [Required]
        public Guid CursoId { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime DtAcesso { get; set; } = DateTime.Now;

        // Navegação (opcional)
        [ForeignKey("FuncionarioId")]
        public Funcionarios? Funcionario { get; set; }

        [ForeignKey("CursoId")]
        public Cursos? Curso { get; set; }
    }
}
