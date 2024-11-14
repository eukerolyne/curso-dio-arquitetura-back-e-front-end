using API.Models;

namespace API.Repositorios.Interfaces
{
    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        void Commit();

        IList<Curso> ObterPorUsuario(int usuarioId);
    }
}
