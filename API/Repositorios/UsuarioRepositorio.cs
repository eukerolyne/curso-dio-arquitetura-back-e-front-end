using API.Data;
using API.Models;
using API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DbContexto _context;

        public UsuarioRepositorio(DbContexto context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Commit()
        {
           _context.SaveChanges();
        }

        public async Task<Usuario> ObterUsuarioAsync(string login)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}
