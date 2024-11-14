using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto.Curso
{
    public class CursoDtoInput
    {
        [Required(ErrorMessage = "O nome do curso é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; }
    }
}
