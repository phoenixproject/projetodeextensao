using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoDeExtensao.BlazorApp._APLICACAO;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using ProjetoDeExtensao.Shared._MODEL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace ProjetoDeExtensao.BlazorApp.Controllers.__AUTENTICACAO
{
	public class AuthController : Controller
	{
		private readonly ProjetodeextensaoContext _projetoextensao;
		private readonly IConfiguration _configuration;

		public AuthController(IConfiguration configuration, ProjetodeextensaoContext projetoextensao)
		{
			_configuration = configuration;
			_projetoextensao = projetoextensao;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginModel credencial)
		{
			HttpClient httpClient = new HttpClient();

			UsuarioDTO usuario = new UsuarioDTO();
			TipoUsuarioDTO tipoUsuario = new TipoUsuarioDTO();

			GestorDeUsuario GestorDeUsuario = new GestorDeUsuario(_projetoextensao);
			GestorDeTipoDeUsuario GestorDeTipoDeUsuario = new GestorDeTipoDeUsuario(_projetoextensao);

			usuario = GestorDeUsuario.ObterUsuarioPorEmailESenha(credencial.email, credencial.password);

			if (usuario == null)
			{
				return Unauthorized();
			}
			else
			{
				try
				{
					tipoUsuario = GestorDeTipoDeUsuario.ObterTipoDeUsuarioPorId(usuario.TpUsuario);

					// Gere o token JWT aqui
					var token = CreateToken(credencial.nome, credencial.email, tipoUsuario.DsTpUsuario);
					return Ok(new { Token = token });

				}
				catch (Exception e)
				{
					return Unauthorized(e.Message.ToString());
				}
			}
		}

		// Exemplo JWT API Tutorial (Modificado)
		private string CreateToken(string name, string email, string permissao)
		{
			//GestorDeAcesso GestorDeAcesso = new GestorDeAcesso();
			//GestorDeAcessoAoCadUnico GestorDeAcessoAoCadUnico = new GestorDeAcessoAoCadUnico(_context);
			//GestorDeCriptografia GestorDeCriptografia = new GestorDeCriptografia();

			//CaduUsuario usuario = GestorDeAcesso.ObterUsuarioPorLogin(username);
			//CaduPermissaoSistema permissao = GestorDeAcesso.ObterNivelDeAcessoASistemaPorIdDeUsuarioESistema(usuario.CdUsuario, 71);

			List<Claim> claims = new List<Claim>
			 {
					new Claim(ClaimTypes.Name, name),
					new Claim(ClaimTypes.Name, email),					
					new Claim(ClaimTypes.Role, permissao)
			 };

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			//GestorDeAcesso.DecodificarJwt(jwt);

			return jwt;
		}
	}
}
