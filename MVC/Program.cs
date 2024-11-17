using Microsoft.AspNetCore.Authentication.Cookies;
using MVC.Handlers;
using MVC.Service;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do Configuration para acessar as vari�veis
var configuration = builder.Configuration;

// Adiciona os servi�os ao container
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var clientHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
};

builder.Services.AddRefitClient<IUsuarioService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(configuration.GetValue<string>("UrlApiCurso"));
    }).ConfigurePrimaryHttpMessageHandler(_ => clientHandler);

builder.Services.AddTransient<BearerTokenMessageHandler>();

builder.Services.AddRefitClient<ICursoService>()
    .AddHttpMessageHandler<BearerTokenMessageHandler>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(configuration.GetValue<string>("UrlApiCurso"));
    }).ConfigurePrimaryHttpMessageHandler(_ => clientHandler);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Logar";
        options.AccessDeniedPath = "/Usuario/Logar";
    });

var app = builder.Build();

// Configura��o do pipeline de requisi��es
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Define o cabe�alho HSTS (Seguran�a para HTTPS)
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
