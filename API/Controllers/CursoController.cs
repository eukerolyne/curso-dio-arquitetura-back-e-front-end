using API.Models;
using API.Models.Dto.Curso;
using API.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ILogger<CursoController> _logger;
        private readonly ICursoRepositorio _cursoRepositorio;

        public CursoController(ILogger<CursoController> logger, ICursoRepositorio cursoRepositorio)
        {
            _logger = logger;
            _cursoRepositorio = cursoRepositorio;
        }

        [HttpPost, Route("")]
        public IActionResult Post(CursoDtoInput cursoDto)
        {
            try
            {
                Curso curso = new Curso
                {
                    Nome = cursoDto.Nome,
                    Descricao = cursoDto.Descricao
                };

                var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                curso.UsuarioId = userId;
                _cursoRepositorio.Adicionar(curso);
                _cursoRepositorio.Commit();

                var cursoOutput = new CursoDtoOutput
                {
                    Nome = curso.Nome,
                    Descricao = curso.Descricao,
                };

                return Created("", cursoOutput);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        [HttpGet, Route("")]
        public IActionResult Get()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                var cursos = _cursoRepositorio.ObterPorUsuario(userId)
                    .Select(s => new CursoDtoOutput()
                    {
                        Nome = s.Nome,
                        Descricao = s.Descricao
                    });

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }
    }
}
