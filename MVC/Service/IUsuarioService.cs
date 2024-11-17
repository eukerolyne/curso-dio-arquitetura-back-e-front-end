using MVC.Models.Usuario;
using Refit;

namespace MVC.Service
{
    public interface IUsuarioService
    {
        [Post("/api/usuario/registrar")]
        Task<RegistrarUsuarioVM> Registrar(RegistrarUsuarioVM registrarUsuarioVM);

        [Post("/api/usuario/login")]
        Task<LoginVmOutput> Logar(LoginVmInput loginVM);
    }
}
