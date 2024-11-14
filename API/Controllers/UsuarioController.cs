using API.Configuracoes;
using API.Models;
using API.Models.Dto.Usuario;
using API.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IAutenticacaoService _authenticationService;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepositorio usuarioRepositorio, IAutenticacaoService authenticationService)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDtoInput login)
        {
            try
            {
                var usuario = await _usuarioRepositorio.ObterUsuarioAsync(login.Login);

                if (usuario == null)
                {
                    return BadRequest("Houve um erro ao tentar acessar.");
                }

                var user = new UsuarioDtoOutput()
                {
                    Id = usuario.Id,
                    Login = login.Login,
                    Email = usuario.Email
                };

                var token = _authenticationService.GerarToken(user);

                return Ok(new LoginDtoOutput
                {
                    Token = token,
                    Usuario = user
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> Registrar(RegistroDtoInput registro)
        {
            try
            {
                var usuario = await _usuarioRepositorio.ObterUsuarioAsync(registro.Login);

                if (usuario != null)
                {
                    return BadRequest("Usuário já cadastrado");
                }

                usuario = new Usuario
                {
                    Login = registro.Login,
                    Senha = registro.Senha,
                    Email = registro.Email
                };
                _usuarioRepositorio.Adicionar(usuario);
                _usuarioRepositorio.Commit();

                return Created("", registro);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }
    }
}
