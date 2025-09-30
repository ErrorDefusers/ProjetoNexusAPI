using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusAPI.Domais
{
    [Table("Setores")]
    public class Setores
    {
        [Key]
        public Guid IdSetor { get; set; }

        [Column (TypeName = "VARCHAR(100)")]
        [Required(ErrorMessage = "O tiposetor é obrigatório!")]
        public string? TipoSetor { get; set; }

    }
}
