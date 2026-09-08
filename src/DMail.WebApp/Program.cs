using DMail.Aplicacao.Modulos.ModuloConfiguracao;
using DMail.Infra;
using DMail.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;
using DMail.WebApp.Servicos;
using Microsoft.AspNetCore.DataProtection;
using DMail.Aplicacao;

var builder = WebApplication.CreateBuilder(args);

// Injeção de dependências
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfraRepositories(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DMailDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DMailConnection")));

// Proteção de chaves
var diretorioDeChaves = Path.Combine(builder.Environment.ContentRootPath, "App_Data", "keys");
Directory.CreateDirectory(diretorioDeChaves);
var dataProtection = builder.Services.AddDataProtection()
    .SetApplicationName("DMail")
    .PersistKeysToFileSystem(new DirectoryInfo(diretorioDeChaves));
if (OperatingSystem.IsWindows())
    dataProtection.ProtectKeysWithDpapi();

builder.Services.AddSingleton<IProtecaoDeSegredo, ProtecaoDeSegredoComDataProtection>();
builder.Services.AddHostedService<AgendadorDeDMailBackgroundService>();

var app = builder.Build();

// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler(new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerOptions
//     {
//         ExceptionHandlingPath = "/Home/Error",
//         AllowStatusCode404Response = true
//     });
//     app.UseHsts();
// }
// else
// {
//     app.UseDeveloperExceptionPage();
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=DMails}/{action=Index}/{id?}");

app.Run();