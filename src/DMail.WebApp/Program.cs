using DMail.Aplicacao.Modulos.ModuloConfiguracao;
using DMail.Dominio.Modulos.ModuloConfiguracao;
using DMail.Dominio.Modulos.ModuloDMail;
using DMail.Infra;
using DMail.Infra.Compartilhado.Orm;
using DMail.Infra.Modulos.ModuloConfiguracao;
using DMail.Infra.Modulos.ModuloDMail;
using Microsoft.EntityFrameworkCore;
using DMail.WebApp.Servicos;
using Microsoft.AspNetCore.DataProtection;
using DMail.Aplicacao;

var builder = WebApplication.CreateBuilder(args);


// Injeção de depedencias
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfraRepositories(builder.Configuration);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DMailDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DMailConnection")));//


//Proteção de chaves
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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=DMails}/{action=Index}/{id?}");

app.Run();
