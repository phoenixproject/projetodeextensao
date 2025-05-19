using Microsoft.AspNetCore.Mvc;
using ProjetoDeExtensao.BlazorApp._APLICACAO;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using ProjetoDeExtensao.Shared._MODEL;

namespace ProjetoDeExtensao.BlazorApp.Controllers._SISTEMA
{
	[Route("api/Usuario")]
	[ApiController]
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
		public async Task<ActionResult<List<UsuarioDTO>>> Get()
		{
			List<UsuarioDTO> usuarios = new List<UsuarioDTO>();

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
		public async Task<ActionResult<UsuarioDTO>> Get(int id)
		{
			UsuarioDTO usuario = new UsuarioDTO();

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
