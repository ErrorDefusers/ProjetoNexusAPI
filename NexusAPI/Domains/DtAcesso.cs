using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusAPI.Domains
{
    [Table("DtAcesso")] 
    public class DtAcesso
    {
        [Key]
        public Guid IdDtAcesso { get; set; } = Guid.NewGuid(); 

        [Required]
        public Guid IdFuncionario { get; set; }

        [Required]
        public Guid IdFerramenta { get; set; }

        [Column(TypeName = "datetime")]
        [Required]
        public DateTime DataAcesso { get; set; } 
    }
}
