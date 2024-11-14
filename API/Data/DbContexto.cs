using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> options)
           : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
