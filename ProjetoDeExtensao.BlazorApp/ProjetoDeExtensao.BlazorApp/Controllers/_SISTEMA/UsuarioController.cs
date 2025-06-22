using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDeExtensao.BlazorApp._APLICACAO;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using ProjetoDeExtensao.Shared._MODEL;
using System.Security.Cryptography;
using System.Text;

namespace ProjetoDeExtensao.BlazorApp.Controllers._SISTEMA
{
	[Route("api/Usuario")]
	[ApiController]
	public class UsuarioController : Controller
	{
		private readonly ProjetodeextensaoContext _projetoextensao;
		private readonly ILogger<UsuarioController> _logger;

		public UsuarioController(ProjetodeextensaoContext projetoextensao, ILogger<UsuarioController> logger = null)
		{
			_projetoextensao = projetoextensao;
			_logger = logger;
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

				// Log para depuração
				Console.WriteLine($"Total de usuários retornados: {usuarios.Count}");
				if (usuarios.Any())
				{
					var primeiro = usuarios.First();
					Console.WriteLine($"Primeiro usuário - ID: {primeiro.CdUsuario}, Nome: {primeiro.Nome}, TpServico: {primeiro.TpServico}, Status: {primeiro.CdStatus}");
				}
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Erro ao obter usuários");
				Console.WriteLine($"Erro ao obter usuários: {ex.Message}");
				return BadRequest(new { message = ex.Message });
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
				usuario =  GestorDeUsuario.ConverterUsuario(GestorDeUsuario.ObterUsuarioPorId(id));

				if (usuario == null || usuario.CdUsuario == 0)
				{
					return NotFound(new { message = $"Usuário com ID {id} não encontrado" });
				}
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, $"Erro ao obter usuário com ID {id}");
				return BadRequest(new { message = ex.Message });
			}

			return usuario;
		}

		// Novo endpoint para buscar usuários por tipo de serviço
		[HttpGet("PorTipoServico/{tipoServicoId}")]
		public async Task<ActionResult<List<UsuarioDTO>>> GetPorTipoServico(int tipoServicoId)
		{
			List<UsuarioDTO> usuarios = new List<UsuarioDTO>();

			try
			{
				GestorDeUsuario GestorDeUsuario = new GestorDeUsuario(_projetoextensao);
				var todosUsuarios = GestorDeUsuario.ObterTodosOsUsuarios();

				// Filtrar por tipo de serviço e status ativo (assumindo que status 1 é ativo)
				usuarios = todosUsuarios
					.Where(u => u.TpServico == tipoServicoId && u.CdStatus == 1)
					.ToList();

				Console.WriteLine($"Usuários filtrados por tipo de serviço {tipoServicoId}: {usuarios.Count}");
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, $"Erro ao filtrar usuários por tipo de serviço {tipoServicoId}");
				Console.WriteLine($"Erro ao filtrar usuários por tipo de serviço: {ex.Message}");
				return BadRequest(new { message = ex.Message });
			}

			return usuarios;
		}

		// Método POST para adicionar um novo usuário
		[HttpPost]
		public async Task<ActionResult<UsuarioDTO>> Post([FromBody] UsuarioDTO usuarioDTO)
		{
			try
			{
				// Validação dos campos obrigatórios
				if (usuarioDTO == null)
				{
					return BadRequest(new { message = "Dados do usuário não fornecidos" });
				}

				if (string.IsNullOrEmpty(usuarioDTO.Nome))
				{
					return BadRequest(new { message = "Nome é obrigatório" });
				}

				if (string.IsNullOrEmpty(usuarioDTO.Email))
				{
					return BadRequest(new { message = "Email é obrigatório" });
				}

				if (string.IsNullOrEmpty(usuarioDTO.Password))
				{
					return BadRequest(new { message = "Senha é obrigatória" });
				}

				if (usuarioDTO.TpUsuario <= 0)
				{
					return BadRequest(new { message = "Tipo de usuário é obrigatório" });
				}

				if (usuarioDTO.CdStatus <= 0)
				{
					return BadRequest(new { message = "Status é obrigatório" });
				}

				// Verificar se já existe um usuário com o mesmo email
				GestorDeUsuario gestorDeUsuario = new GestorDeUsuario(_projetoextensao);
				var usuarioExistente = gestorDeUsuario.ObterUsuarioPorEmail(usuarioDTO.Email);

				if (usuarioExistente != null && usuarioExistente.CdUsuario > 0)
				{
					return BadRequest(new { message = $"Já existe um usuário com o email {usuarioDTO.Email}" });
				}

				Usuario usuario = gestorDeUsuario.ConverterUsuario(usuarioDTO);

				// Adicionar o novo usuário
				var novoUsuario = gestorDeUsuario.InserirUsuario(usuario);

				if (novoUsuario == null || usuario.CdUsuario <= 0)
				{
					return StatusCode(500, new { message = "Erro ao adicionar usuário" });
				}

				_logger?.LogInformation($"Usuário adicionado com sucesso: ID {usuario.CdUsuario}, Nome: {usuario.Nome}");
				Console.WriteLine($"Usuário adicionado com sucesso: ID {usuario.CdUsuario}, Nome: {usuario.Nome}");

				return CreatedAtAction(nameof(Get), new { id = usuario.CdUsuario }, novoUsuario);
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Erro ao adicionar usuário");
				Console.WriteLine($"Erro ao adicionar usuário: {ex.Message}");
				return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
			}
		}

		// Método PUT para atualizar um usuário existente
		[HttpPut("{id}")]
		public async Task<ActionResult<UsuarioDTO>> Put(int id, [FromBody] UsuarioDTO usuarioDTO)
		{
			try
			{
				// Validação dos campos obrigatórios
				if (usuarioDTO == null)
				{
					return BadRequest(new { message = "Dados do usuário não fornecidos" });
				}

				if (id != usuarioDTO.CdUsuario)
				{
					return BadRequest(new { message = "ID do usuário não corresponde ao ID na URL" });
				}

				if (string.IsNullOrEmpty(usuarioDTO.Nome))
				{
					return BadRequest(new { message = "Nome é obrigatório" });
				}

				if (string.IsNullOrEmpty(usuarioDTO.Email))
				{
					return BadRequest(new { message = "Email é obrigatório" });
				}

				if (usuarioDTO.TpUsuario <= 0)
				{
					return BadRequest(new { message = "Tipo de usuário é obrigatório" });
				}

				if (usuarioDTO.CdStatus <= 0)
				{
					return BadRequest(new { message = "Status é obrigatório" });
				}

				// Verificar se o usuário existe
				GestorDeUsuario gestorDeUsuario = new GestorDeUsuario(_projetoextensao);
				Usuario usuarioExistente = gestorDeUsuario.ObterUsuarioPorId(id);

				if (usuarioExistente == null || usuarioExistente.CdUsuario <= 0)
				{
					return NotFound(new { message = $"Usuário com ID {id} não encontrado" });
				}

				// Verificar se o email já está em uso por outro usuário
				if (usuarioDTO.Email != usuarioExistente.Email)
				{
					var usuarioComMesmoEmail = gestorDeUsuario.ObterUsuarioPorEmail(usuarioDTO.Email);
					if (usuarioComMesmoEmail != null && usuarioComMesmoEmail.CdUsuario > 0 && usuarioComMesmoEmail.CdUsuario != id)
					{
						return BadRequest(new { message = $"O email {usuarioDTO.Email} já está em uso por outro usuário" });
					}
				}

				// Se a senha estiver vazia, manter a senha atual
				if (usuarioDTO.Password != usuarioExistente.Password)
				{
					usuarioExistente.Password = usuarioDTO.Password;
				}

				//Usuario usuario = gestorDeUsuario.ConverterUsuario(usuarioDTO);

				// Atualizar o usuário
				var usuarioAtualizado = gestorDeUsuario.AtualizarUsuario(usuarioExistente);

				if (usuarioAtualizado == null || usuarioExistente.CdUsuario <= 0)
				{
					return StatusCode(500, new { message = "Erro ao atualizar usuário" });
				}

				_logger?.LogInformation($"Usuário atualizado com sucesso: ID {usuarioExistente.CdUsuario}, Nome: {usuarioExistente.Nome}");
				Console.WriteLine($"Usuário atualizado com sucesso: ID {usuarioExistente.CdUsuario}, Nome: {usuarioExistente.Nome}");

				return Ok(usuarioAtualizado);
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, $"Erro ao atualizar usuário com ID {id}");
				Console.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
				return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
			}
		}

		// Método DELETE para excluir um usuário
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				// Verificar se o usuário existe
				GestorDeUsuario gestorDeUsuario = new GestorDeUsuario(_projetoextensao);
				var usuario = gestorDeUsuario.ObterUsuarioPorId(id);

				if (usuario == null || usuario.CdUsuario <= 0)
				{
					return NotFound(new { message = $"Usuário com ID {id} não encontrado" });
				}

				// Excluir o usuário
				bool sucesso = gestorDeUsuario.RemoverUsuario(id);

				if (!sucesso)
				{
					return StatusCode(500, new { message = "Erro ao excluir usuário" });
				}

				_logger?.LogInformation($"Usuário excluído com sucesso: ID {id}, Nome: {usuario.Nome}");
				Console.WriteLine($"Usuário excluído com sucesso: ID {id}, Nome: {usuario.Nome}");

				return NoContent();
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, $"Erro ao excluir usuário com ID {id}");
				Console.WriteLine($"Erro ao excluir usuário: {ex.Message}");
				return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
			}
		}
	}
}
