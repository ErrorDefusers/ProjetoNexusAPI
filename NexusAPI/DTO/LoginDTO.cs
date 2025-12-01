
using System.ComponentModel.DataAnnotations;

namespace NexusAPI.DTO
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "O campo de Senha é obrigatório.")]
        public string? Password { get; set; }

    }
}
