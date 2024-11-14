using API.Models;
using API.Data;
using API.Repositorios.Interfaces;

namespace API.Repositorios
{
    public class CursoRepositorio : ICursoRepositorio
    {
        private readonly DbContexto _context;

        public CursoRepositorio(DbContexto context)
        {
            _context = context;
        }

        public void Adicionar(Curso curso)
        {
           _context.Cursos.Add(curso);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Curso> ObterPorUsuario(int usuarioId)
        {
            var cursos = _context.Cursos.Where(c => c.UsuarioId == usuarioId).ToList();
            return cursos;
        }
    }
}
