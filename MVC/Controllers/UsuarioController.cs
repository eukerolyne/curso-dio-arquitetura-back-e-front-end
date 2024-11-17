using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Usuario;
using MVC.Service;
using Refit;
using System.Security.Claims;

namespace MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(RegistrarUsuarioVM registrarUsuarioVM)
        {
            try
            {
                var usuario = await _usuarioService.Registrar(registrarUsuarioVM);

                ModelState.AddModelError("", $"Os dados foram cadastrado com sucesso para o login {usuario.Login}");
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            //var clientHandler = new HttpClientHandler();
            //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            //var httpClient = new HttpClient(clientHandler);
            //httpClient.BaseAddress = new Uri("https://localhost:5001/");
            //var registrarUsuarioViewModelInputJson =  JsonConvert.SerializeObject(registrarUsuarioViewModelInput);
            //var httpContent = new StringContent(registrarUsuarioViewModelInputJson, Encoding.UTF8, "application/json");

            //var httpPost = httpClient.PostAsync("/api/v1/usuario/registrar", httpContent).GetAwaiter().GetResult();

            //if(httpPost.StatusCode == System.Net.HttpStatusCode.Created)
            //{
            //    ModelState.AddModelError("", "Os dados foram cadastrado com sucesso");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Erro ao cadastrar");
            //}

            return View();
        }

        public IActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logar(LoginVmInput loginVm)
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                LoginVmOutput usuario = await _usuarioService.Logar(loginVm);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Usuario.Login),
                    new Claim(ClaimTypes.Email, usuario.Usuario.Email),
                    new Claim("token", usuario.Token),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddDays(1))
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                ModelState.AddModelError("", $"O usuário está autenticado {usuario.Token}");
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

        public IActionResult EfetuarLogoff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction($"{nameof(Logar)}");
        }
    }
}