using Microsoft.AspNetCore.Mvc;
using ProjetoDeExtensao.BlazorApp._APLICACAO;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;

namespace ProjetoDeExtensao.BlazorApp.Controllers._SISTEMA
{
	public class UsuarioController : Controller
	{
		private readonly ProjetodeextensaoContext _projetoextensao;

		public UsuarioController(ProjetodeextensaoContext projetoextensao)
		{
			_projetoextensao = projetoextensao;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<ActionResult<List<Usuario>>> Get()
		{
			List<Usuario> usuarios = new List<Usuario>();

			try
			{
				GestorDeUsuario GestorDeUsuario = new GestorDeUsuario(_projetoextensao);
				usuarios = GestorDeUsuario.ObterTodosOsUsuarios();
			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}

			return usuarios;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Usuario>> Get(int id)
		{
			Usuario usuario = new Usuario();

			try
			{
				GestorDeUsuario GestorDeUsuario = new GestorDeUsuario(_projetoextensao);
				usuario = GestorDeUsuario.ObterUsuarioPorId(id);
			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}

			return usuario;
		}
	}
}
