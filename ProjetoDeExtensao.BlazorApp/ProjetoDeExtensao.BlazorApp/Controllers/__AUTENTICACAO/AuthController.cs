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
	[ApiController]
	[Route("api/auth")]
	public class AuthController : Controller
	{
		private readonly ProjetodeextensaoContext _projetoextensao;
		private readonly IConfiguration _configuration;
		private readonly ILogger<AuthController> _logger;

		public AuthController(IConfiguration configuration, ProjetodeextensaoContext projetoextensao, ILogger<AuthController> logger)
		{
			_configuration = configuration;
			_projetoextensao = projetoextensao;
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
		{
			_logger.LogInformation($"Tentativa de login para: {loginModel.email}");

			// Validar campos obrigatórios
			if (string.IsNullOrEmpty(loginModel.email) || string.IsNullOrEmpty(loginModel.password))
			{
				_logger.LogWarning("Campos obrigatórios ausentes no payload de login");
				return BadRequest("Email e senha são obrigatórios");
			}

			// Se o nome não foi fornecido, use parte do email como nome
			if (string.IsNullOrEmpty(loginModel.nome))
			{
				_logger.LogInformation("Campo 'nome' não fornecido, usando parte do email");
				loginModel.nome = loginModel.email.Split('@')[0];
			}

			HttpClient httpClient = new HttpClient();

			UsuarioDTO usuario = new UsuarioDTO();
			TipoUsuarioDTO tipoUsuario = new TipoUsuarioDTO();

			GestorDeUsuario GestorDeUsuario = new GestorDeUsuario(_projetoextensao);
			GestorDeTipoDeUsuario GestorDeTipoDeUsuario = new GestorDeTipoDeUsuario(_projetoextensao);

			try
			{
				if (GestorDeUsuario.VerificarSeExisteUsuarioPorEmailESenha(loginModel.email, loginModel.password))
				{
					usuario = GestorDeUsuario.ObterUsuarioPorEmailESenha(loginModel.email, loginModel.password);

					if (usuario == null)
					{
						_logger.LogWarning($"Usuário não encontrado para o email: {loginModel.email}");
						return Unauthorized("Email ou senha incorretos");
					}
					else
					{
						tipoUsuario = GestorDeTipoDeUsuario.ObterTipoDeUsuarioPorId(usuario.TpUsuario);

						if(tipoUsuario.DsTpUsuario == "Admin")
						{
							_logger.LogInformation($"Usuário autenticado: {loginModel.email}, Tipo: {tipoUsuario.DsTpUsuario}");

							// Gere o token JWT aqui
							var token = CreateToken(usuario.Nome, loginModel.email, tipoUsuario.DsTpUsuario);
							return Ok(new { Token = token });
						}
						else
						{
							_logger.LogWarning($"Tipo de usuário não encontrado para o ID: {usuario.TpUsuario}");
							return Unauthorized("Tipo de usuário inválido");
						}
					}
				}
				else
				{
					_logger.LogWarning($"Usuário não encontrado para o email: {loginModel.email}");
					return Unauthorized("Email ou senha incorretos");
				}
			}
			catch (Exception e)
			{
				_logger.LogError($"Erro durante autenticação: {e.Message}");
				return StatusCode(500, $"Erro interno: {e.Message}");
			}
		}

		// Exemplo JWT API Tutorial (Modificado)
		private string CreateToken(string name, string email, string permissao)
		{
			try
			{
				List<Claim> claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, name),
					new Claim(ClaimTypes.Email, email),
					new Claim(ClaimTypes.Role, permissao)
				};

				// Obter a chave do appsettings.json
				string tokenValue = _configuration.GetSection("AppSettings:Token").Value;

				// Verificar se a chave é longa o suficiente
				if (string.IsNullOrEmpty(tokenValue) || tokenValue.Length < 64)
				{
					_logger.LogWarning("A chave JWT é muito curta. Usando uma chave padrão segura temporária.");
					// Usar uma chave temporária longa o suficiente (apenas para desenvolvimento)
					tokenValue = "minhachavesupersecretacomtamanhosuficienteparaoalgoritmohmacsha512depeloMenos64caracteres";
				}

				// Usar o algoritmo HmacSha256 que requer uma chave menor (32 bytes)
				var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenValue));
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

				var token = new JwtSecurityToken(
					claims: claims,
					expires: DateTime.Now.AddDays(1),
					signingCredentials: creds);

				var jwt = new JwtSecurityTokenHandler().WriteToken(token);
				return jwt;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Erro ao criar token JWT: {ex.Message}");
				throw;
			}
		}
	}
}
