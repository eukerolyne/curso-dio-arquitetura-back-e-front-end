using API.Models.Dto.Usuario;
using AutoBogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Tests.Configurations;
using Xunit.Abstractions;

namespace Tests.Integrations.Controllers
{
    public class UsuarioControllerTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Program> _factory;
        protected readonly ITestLoggerFactory _output;
        protected readonly HttpClient _httpClient;
        protected RegistroDtoInput RegistroDtoInput;
        protected LoginDtoOutput LoginDtoOutput;

        public UsuarioControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = new TestLoggerFactory(output);
            _httpClient = _factory.CreateClient();
            RegistroDtoInput = new RegistroDtoInput();
            LoginDtoOutput = new LoginDtoOutput();
        }

        [Fact]
        public async Task Registrar_InformandoUsuarioESenha_DeveRetornarSucesso()
        {
            // Arrange
            RegistroDtoInput = new AutoFaker<RegistroDtoInput>(AutoBogusConfiguration.LOCATE)
                                .RuleFor(p => p.Login, faker => faker.Person.UserName)
                                .RuleFor(p => p.Email, faker => faker.Person.Email)
                                .RuleFor(p => p.Senha, faker => faker.Internet.Password());

            StringContent content = new StringContent(JsonConvert.SerializeObject(RegistroDtoInput), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/usuario/registrar", content);

            // Assert
            _output.WriteLine($"Resultado: {await response.Content.ReadAsStringAsync()}");
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Logar_InformandoUsuarioESenhaExistentes_DeveRetornarSucesso()
        {
            // Arrange
            var loginDtoInput = new LoginDtoInput
            {
                Login = RegistroDtoInput.Login,
                Senha = RegistroDtoInput.Senha
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginDtoInput), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/usuario/login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.False(string.IsNullOrWhiteSpace(responseContent), "A resposta não possui conteúdo.");
            LoginDtoOutput = JsonConvert.DeserializeObject<LoginDtoOutput>(responseContent) ?? new LoginDtoOutput();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(LoginDtoOutput.Token);
            Assert.NotNull(LoginDtoOutput.Usuario);
            Assert.Equal(loginDtoInput.Login, LoginDtoOutput.Usuario.Login);

            _output.WriteLine($"Resultado: {responseContent}");
        }

        public async Task InitializeAsync()
        {
            RegistroDtoInput = new AutoFaker<RegistroDtoInput>(AutoBogusConfiguration.LOCATE)
                                .RuleFor(p => p.Login, faker => faker.Person.UserName)
                                .RuleFor(p => p.Email, faker => faker.Person.Email)
                                .RuleFor(p => p.Senha, faker => faker.Internet.Password());

            await Registrar_InformandoUsuarioESenha_DeveRetornarSucesso();
        }

        public Task DisposeAsync()
        {
            _httpClient.Dispose();
            return Task.CompletedTask;
        }
    }
}
