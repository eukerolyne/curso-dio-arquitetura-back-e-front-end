using MVC.Models.Curso;
using Refit;

namespace MVC.Service
{
    public interface ICursoService
    {
        [Post("/api/curso")]
        [Headers("Authorization: Bearer")]
        Task<CadastrarCursoVMInput> Registrar(CadastrarCursoVMInput cadastrarCursoVM);

        [Get("/api/curso")]
        [Headers("Authorization: Bearer")]
        Task<IList<ListarCursoVM>> Obter();
    }
}
