using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusAPI.Domains
{
    [Table("Cursos")]
    public class Cursos
    {
        [Key]
        public Guid IdCurso { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        
        public string? IdExterno { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(250)")]
        [Required(ErrorMessage = "O título é obrigatório!")]
        public string Titulo { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(500)")]
        public string? Descricao { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(500)")]
        [Required(ErrorMessage = "A URL é obrigatória!")]
        public string Url { get; set; } = string.Empty;

        [Column(TypeName = "Float")]
        
        public float? Progresso { get; set; } = 0;

        public Cursos() { }
    }
}
