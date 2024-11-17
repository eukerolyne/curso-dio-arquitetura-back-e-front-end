using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Usuario
{
    public class LoginVmInput
    {
        [Required(ErrorMessage = "O login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
    }
}
