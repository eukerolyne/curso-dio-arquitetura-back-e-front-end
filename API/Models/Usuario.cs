using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public ICollection<Curso> Cursos { get; set; }
    }
}
