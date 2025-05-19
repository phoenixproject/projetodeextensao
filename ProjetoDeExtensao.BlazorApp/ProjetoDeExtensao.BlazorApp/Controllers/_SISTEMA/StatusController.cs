using Microsoft.AspNetCore.Mvc;
using ProjetoDeExtensao.BlazorApp._APLICACAO;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;

namespace ProjetoDeExtensao.BlazorApp.Controllers._SISTEMA
{
	[Route("api/Status")]
	[ApiController]
	public class StatusController : Controller
	{
		private readonly ProjetodeextensaoContext _projetoextensao;

		public StatusController(ProjetodeextensaoContext projetoextensao)
		{
			_projetoextensao = projetoextensao;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<ActionResult<List<Status>>> Get()
		{
			List<Status> statuss = new List<Status>();

			try
			{
				GestorDeStatus GestorDeStatus = new GestorDeStatus(_projetoextensao);
				statuss = GestorDeStatus.ObterTodosOsStatus();
			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}

			return statuss;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Status>> Get(int id)
		{
			Status status = new Status();

			try
			{
				GestorDeStatus GestorDeStatus = new GestorDeStatus(_projetoextensao);
				status = GestorDeStatus.ObterStatusPorId(id);
			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}

			return status;
		}
	}
}
