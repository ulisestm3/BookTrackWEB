using DAL;
using Microsoft.EntityFrameworkCore;
using Radzen;
using UI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
#region dbcontext
builder.Services.AddDbContextFactory<DAL.dbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion



#region Services
builder.Services.AddScoped<DAL.IAuthenticationController, DAL.AuthenticationController>();
builder.Services.AddScoped<DAL.IFormulariosController, DAL.FormulariosController>();
builder.Services.AddScoped<DAL.IParametrosController, DAL.ParametrosController>();
builder.Services.AddScoped<DAL.IPermisoFormularioController, DAL.PermisoFormularioController>();
builder.Services.AddScoped<DAL.IPermisosController, DAL.PermisosController>();
builder.Services.AddScoped<DAL.IReportesController, DAL.ReportesController>();
builder.Services.AddScoped<DAL.IUsuarioFormulariosController, DAL.UsuarioFormulariosController>();
builder.Services.AddScoped<DAL.IUsuarioPermisosController, DAL.UsuarioPermisosController>();
builder.Services.AddScoped<DAL.IUsuarioReportesController, DAL.UsuarioReportesController>();
builder.Services.AddScoped<DAL.IUsuariosController, DAL.UsuariosController>();
builder.Services.AddScoped<DAL.IvFormulariosUsuarioController, DAL.vFormulariosUsuarioController>();
builder.Services.AddScoped<DAL.IvPermisosUsuarioController, DAL.vPermisosUsuarioController>();
builder.Services.AddScoped<DAL.IvUsuarioFormulariosController, DAL.vUsuarioFormulariosController>();
builder.Services.AddScoped<DAL.IvUsuariosController, DAL.vUsuariosController>();
#endregion


builder.Services.AddScoped<DAL.IUsuarioFormulariosSPController, DAL.UsuarioFormulariosSPController>();
builder.Services.AddScoped<DAL.IUsuarioPermisosSPController, DAL.UsuarioPermisosSPController>();




builder.Services.AddScoped<Utility.IGeneral, Utility.General>();
builder.Services.AddScoped<Utility.IPasswordUtility, Utility.PasswordUtility>();
builder.Services.AddScoped<Utility.ISHA256, Utility.SHA256>();
builder.Services.AddScoped<DAL.IAuthenticationController, DAL.AuthenticationController>();
builder.Services.AddScoped<UI.Authentication.IAutenticationsServices, UI.Authentication.AutenticationsServices>();
builder.Services.AddScoped<UI.IRadzenMessages, UI.RadzenMessages>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.MaximumReceiveMessageSize = 4 * 1024 * 1024; // 4 MB
    });



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();