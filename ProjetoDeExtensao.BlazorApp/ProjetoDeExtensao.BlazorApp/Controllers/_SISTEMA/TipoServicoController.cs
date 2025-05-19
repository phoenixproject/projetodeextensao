using Microsoft.AspNetCore.Mvc;
using ProjetoDeExtensao.BlazorApp._APLICACAO;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using ProjetoDeExtensao.Shared._MODEL;

namespace ProjetoDeExtensao.BlazorApp.Controllers._SISTEMA
{
	[Route("api/TipoServico")]
	[ApiController]
	public class TipoServicoController : Controller
	{
		private readonly ProjetodeextensaoContext _projetoextensao;

		public TipoServicoController(ProjetodeextensaoContext projetoextensao)
		{
			_projetoextensao = projetoextensao;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<ActionResult<List<TipoServicoDTO>>> Get()
		{
			List<TipoServicoDTO> tipoServicos = new List<TipoServicoDTO>();

			try
			{
				GestorDeTipoDeServico GestorDeTipoDeServico = new GestorDeTipoDeServico(_projetoextensao);
				tipoServicos = GestorDeTipoDeServico.ObterTodosOsTiposDeServicos();
			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}

			return tipoServicos;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TipoServicoDTO>> Get(int id)
		{
			TipoServicoDTO tipoServico = new TipoServicoDTO();

			try
			{
				GestorDeTipoDeServico GestorDeTipoDeServico = new GestorDeTipoDeServico(_projetoextensao);
				tipoServico = GestorDeTipoDeServico.ObterTipoDeServicoPorId(id);
			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}

			return tipoServico;
		}
	}
}
