using API.Models.Dto.Usuario;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Configuracoes
{
    public class JwtService : IAutenticacaoService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(UsuarioDtoOutput usuarioDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioDto.Id.ToString()),
                new Claim(ClaimTypes.Name, usuarioDto.Login.ToString()),
                new Claim(ClaimTypes.Email, usuarioDto.Email.ToString())
            };

            var tokenDescriptor = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    expires: DateTime.UtcNow.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var token = tokenHandler.WriteToken(tokenDescriptor);

            return token;
        }
    }
}