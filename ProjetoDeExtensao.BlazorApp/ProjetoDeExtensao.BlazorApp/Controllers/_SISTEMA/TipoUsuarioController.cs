using Microsoft.AspNetCore.Mvc;
using ProjetoDeExtensao.BlazorApp._APLICACAO;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;

namespace ProjetoDeExtensao.BlazorApp.Controllers._SISTEMA
{
	public class TipoUsuarioController : Controller
	{
		private readonly ProjetodeextensaoContext _projetoextensao;

		public TipoUsuarioController(ProjetodeextensaoContext projetoextensao)
		{
			_projetoextensao = projetoextensao;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<ActionResult<List<TipoUsuario>>> Get()
		{
			List<TipoUsuario> tipoUsuarios = new List<TipoUsuario>();

			try
			{
				GestorDeTipoDeUsuario GestorDeTipoDeUsuario = new GestorDeTipoDeUsuario(_projetoextensao);
				tipoUsuarios = GestorDeTipoDeUsuario.ObterTodosOsTiposDeUsuarios();
			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}

			return tipoUsuarios;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TipoUsuario>> Get(int id)
		{
			TipoUsuario tipoUsuario = new TipoUsuario();

			try
			{
				GestorDeTipoDeUsuario GestorDeTipoDeUsuario = new GestorDeTipoDeUsuario(_projetoextensao);
				tipoUsuario = GestorDeTipoDeUsuario.ObterTipoDeUsuarioPorId(id);
			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}

			return tipoUsuario;
		}
	}
}
