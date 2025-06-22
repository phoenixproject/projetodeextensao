using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using ProjetoDeExtensao.BlazorApp.Client.Pages;
using ProjetoDeExtensao.BlazorApp.Components;
using System.Text;

namespace ProjetoDeExtensao.BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

			builder.Services.AddControllers();

			builder.Services.AddScoped<HttpClient>();

			// Configurar autenticação JWT
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			});

			// Necessário para completar a validação do token nas APIs com
			// parâmetros [Authorize] nos controllers.
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
							.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
						ValidateIssuer = false,
						ValidateAudience = false,

						ValidateLifetime = true,
						ValidIssuer = "yourissuer",
						ValidAudience = "youraudience"
					};
				});

			// Para a autenticação com JWT
			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages();

			// Adicionar suporte ao appsettings.json e variáveis de ambiente
			builder.Configuration
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables(); // Variáveis de ambiente sobrescrevem o appsettings.json

			builder.Services.AddDbContext<ProjetodeextensaoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProjetoDeExtensaoConnection")));

			var app = builder.Build();

			app.UseDeveloperExceptionPage();

			// Para a autenticação com JWT
			app.UseAuthentication();
			app.UseAuthorization();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

			app.MapControllers();

			app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
