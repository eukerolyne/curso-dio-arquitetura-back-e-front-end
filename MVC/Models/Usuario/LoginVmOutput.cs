namespace MVC.Models.Usuario
{
    public class LoginVmOutput
    {
        public string Token { get; set; }
        public LoginViewModelDetalhesOutput Usuario { get; set; }
    }
    public class LoginViewModelDetalhesOutput
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}