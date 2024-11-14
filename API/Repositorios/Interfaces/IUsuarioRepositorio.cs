using API.Models;

namespace API.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        void Adicionar(Usuario usuario);
        void Commit();
        Task<Usuario> ObterUsuarioAsync(string login);
    }
}
