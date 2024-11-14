using API.Models.Dto.Usuario;

namespace API.Configuracoes
{
    public interface IAutenticacaoService
    {
        string GerarToken(UsuarioDtoOutput usuarioDto);
    }
}
