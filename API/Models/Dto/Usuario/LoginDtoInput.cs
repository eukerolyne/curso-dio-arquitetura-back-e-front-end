using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto.Usuario
{
    public class LoginDtoInput
    {
        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
    }
}
