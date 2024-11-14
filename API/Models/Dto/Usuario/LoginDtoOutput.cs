namespace API.Models.Dto.Usuario
{
    public class LoginDtoOutput
    {
        public string Token { get; set; }

        public UsuarioDtoOutput Usuario { get; set; }
    }
}
