using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusAPI.Domais
{
    [Table ("Funcionarios")]
    public class Funcionarios
    {
        [Key]
        public Guid IdFuncionario { get; set; }

        [ForeignKey("TiposFuncionarios")]
        public TiposFuncionarios? TipoDeFuncionario { get; set; }

        [ForeignKey("Setores")]
        public Setores? TipoSetor { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        [Required(ErrorMessage = "O nome do funcionário é obrigátorio")]
        public string? Nome { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        [Required(ErrorMessage = "O email do funcionário é obrigátorio")]
        public string? Email { get; set; }

        [Column(TypeName = "VARCHAR(60)")]
        [Required(ErrorMessage = "A senha do funcionário é obrigátoria!")]
        public string? Senha { get; set; }

        [Column(TypeName = "DATE")]
        [Required(ErrorMessage = "A idade é obrigátoria")]
        public DateOnly Idade { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        [Required(ErrorMessage = "O cargo é obrigátorio!")]
        public string? Cargo { get; set; }
    }
}
