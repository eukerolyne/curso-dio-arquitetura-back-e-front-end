using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        
        public int UsuarioId { get; set; }        
        public Usuario Usuario { get; set; }
    }
}
