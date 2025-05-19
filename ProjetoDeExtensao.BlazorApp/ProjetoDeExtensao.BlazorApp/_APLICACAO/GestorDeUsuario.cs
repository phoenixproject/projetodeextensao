using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using ProjetoDeExtensao.BlazorApp._REPOSITORIO;
using ProjetoDeExtensao.Shared._MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDeExtensao.BlazorApp._APLICACAO
{
	public class GestorDeUsuario
	{
		public ProjetodeextensaoContext _projetoextensao { get; set; }
		public RepositorioDeUsuario RepositorioDeUsuario { get; set; }

		public GestorDeUsuario(ProjetodeextensaoContext projetoextensao)
		{
			this.RepositorioDeUsuario = new RepositorioDeUsuario(projetoextensao);
		}

		public UsuarioDTO ConverterUsuarioParaFormatoDTO(Usuario usuario)
		{
			UsuarioDTO usuario_dto = new UsuarioDTO();

			usuario_dto.CdUsuario = usuario.CdUsuario;
			usuario_dto.Email = usuario.Email;
			usuario_dto.Password = usuario.Password;	
			usuario_dto.Nome = usuario.Nome;
			usuario_dto.Telefone = usuario.Telefone;
			usuario_dto.TpUsuario = usuario.TpUsuario;	
			usuario_dto.TpServico = usuario.TpServico;
			usuario_dto.CdStatus = usuario.CdStatus;
			
			return usuario_dto;
		}

		public List<UsuarioDTO> ConverterListaDeUsuarioParaFormatoDTO(List<Usuario> listaDeUsuario)
		{
			List<UsuarioDTO> listaDeUsuarioDTO = new List<UsuarioDTO>();
			foreach (var usuario in listaDeUsuario)
			{
				listaDeUsuarioDTO.Add(ConverterUsuarioParaFormatoDTO(usuario));
			}
			return listaDeUsuarioDTO;
		}

		public UsuarioDTO ObterUsuarioPorId(int id)
		{
			return ConverterUsuarioParaFormatoDTO(RepositorioDeUsuario.ObterUsuarioPorId(id));
		}

		public UsuarioDTO ObterUsuarioPorEmailESenha(string email, string password)
		{
			return ConverterUsuarioParaFormatoDTO(RepositorioDeUsuario.ObterUsuarioPorEmailESenha(email, password));
		}

		public List<UsuarioDTO> ObterTodosOsUsuarios()
		{
			return ConverterListaDeUsuarioParaFormatoDTO(RepositorioDeUsuario.ObterTodosOsUsuarios());
		}

		public UsuarioDTO ObterUsuarioPorEmail(string email)
		{
			return ConverterUsuarioParaFormatoDTO(RepositorioDeUsuario.ObterUsuarioPorEmail(email));
		}

		public bool VerificarSeUsuarioComMesmoEmailJaExiste(Usuario usuario)
		{
			return RepositorioDeUsuario.VerificarSeUsuarioComMesmoEmailJaExiste(usuario);
		}

		public void InserirUsuario(Usuario usuario)
		{
			RepositorioDeUsuario.InserirUsuario(usuario);	
		}

		public int BuscarQuantidadeRegistros()
		{
			return RepositorioDeUsuario.BuscarQuantidadeRegistros();
		}

		public void RemoverUsuario(Usuario usuario)
		{
			RepositorioDeUsuario.RemoverUsuario(usuario);
		}

		public void AtualizarUsuario(Usuario usuario)
		{
			RepositorioDeUsuario.AtualizarUsuario(usuario);
		}
	}
}
