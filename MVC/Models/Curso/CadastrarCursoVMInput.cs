using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Curso
{
    public class CadastrarCursoVMInput
    {
        [Required(ErrorMessage = "O nome do curso é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do curso é obrigatória")]
        public string Descricao { get; set; }
    }
}
