using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusAPI.Domais
{
    [Table("FuncionariosFerramentas")]
    public class FuncionariosFerramentas
    {
        [Key]
        public Guid IdFuncionarioFerramenta { get; set; }

        [ForeignKey("Funcionarios")]
        public Funcionarios? Funcionario { get; set; }

        [ForeignKey("Ferramentas")]
        public Ferramentas? Ferramenta { get; set; }
    }
}
