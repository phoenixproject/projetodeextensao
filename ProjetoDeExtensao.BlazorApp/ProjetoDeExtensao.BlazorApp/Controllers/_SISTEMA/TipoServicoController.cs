using Microsoft.AspNetCore.Mvc;
using ProjetoDeExtensao.BlazorApp._APLICACAO;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;

namespace ProjetoDeExtensao.BlazorApp.Controllers._SISTEMA
{
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
		public async Task<ActionResult<List<TipoServico>>> Get()
		{
			List<TipoServico> tipoServicos = new List<TipoServico>();

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
		public async Task<ActionResult<TipoServico>> Get(int id)
		{
			TipoServico tipoServico = new TipoServico();

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
