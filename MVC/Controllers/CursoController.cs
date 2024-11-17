using Microsoft.AspNetCore.Mvc;
using MVC.Models.Curso;
using MVC.Service;
using Refit;

namespace MVC.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarCursoVMInput cadastrarCursoVM)
        {
            try
            {
                var curso = await _cursoService.Registrar(cadastrarCursoVM);

                ModelState.AddModelError("", $"O curso foi cadastrado com sucesso {curso.Nome}");
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> Listar()
        {
            var cursos = await _cursoService.Obter();

            return View(cursos);
        }
    }
}