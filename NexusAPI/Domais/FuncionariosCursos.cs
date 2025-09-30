using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusAPI.Domais
{
    [Table ("FuncionariosCursos")]
    public class FuncionariosCursos
    {
        [Key]
        public Guid IdFuncionarioCurso { get; set; }

        [ForeignKey("Cursos")]
        public Cursos? Curso { get; set; }

        [ForeignKey("Funcionarios")]
        public Funcionarios? Funcionario { get; set; }
    }
}
