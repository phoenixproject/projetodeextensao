using Microsoft.AspNetCore.Mvc;
using ProjetoDeExtensao.BlazorApp._APLICACAO;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using ProjetoDeExtensao.Shared._MODEL;

namespace ProjetoDeExtensao.BlazorApp.Controllers._SISTEMA
{
	[Route("api/TipoUsuario")]
	[ApiController]
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
		public async Task<ActionResult<List<TipoUsuarioDTO>>> Get()
		{
			List<TipoUsuarioDTO> tipoUsuarios = new List<TipoUsuarioDTO>();

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
		public async Task<ActionResult<TipoUsuarioDTO>> Get(int id)
		{
			TipoUsuarioDTO tipoUsuario = new TipoUsuarioDTO();

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
