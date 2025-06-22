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

		public UsuarioDTO ConverterUsuario(Usuario usuario)
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

		public Usuario ConverterUsuario(UsuarioDTO usuarioDTO)
		{
			Usuario usuario = new Usuario();
			usuario.CdUsuario = usuarioDTO.CdUsuario;
			usuario.Email = usuarioDTO.Email;
			usuario.Password = usuarioDTO.Password;
			usuario.Nome = usuarioDTO.Nome;
			usuario.Telefone = usuarioDTO.Telefone;
			usuario.TpUsuario = usuarioDTO.TpUsuario;
			usuario.TpServico = usuarioDTO.TpServico;
			usuario.CdStatus = usuarioDTO.CdStatus;
			return usuario;
		}

		public List<UsuarioDTO> ConverterListaDeUsuarioParaFormatoDTO(List<Usuario> listaDeUsuario)
		{
			List<UsuarioDTO> listaDeUsuarioDTO = new List<UsuarioDTO>();
			foreach (var usuario in listaDeUsuario)
			{
				listaDeUsuarioDTO.Add(ConverterUsuario(usuario));
			}
			return listaDeUsuarioDTO;
		}

		public Usuario ObterUsuarioPorId(int id)
		{
			return RepositorioDeUsuario.ObterUsuarioPorId(id);
		}

		public UsuarioDTO ObterUsuarioPorEmailESenha(string email, string password)
		{
			return ConverterUsuario(RepositorioDeUsuario.ObterUsuarioPorEmailESenha(email, password));
		}

		public List<UsuarioDTO> ObterTodosOsUsuarios()
		{
			return ConverterListaDeUsuarioParaFormatoDTO(RepositorioDeUsuario.ObterTodosOsUsuarios());
		}

		public UsuarioDTO ObterUsuarioPorEmail(string email)
		{
			return ConverterUsuario(RepositorioDeUsuario.ObterUsuarioPorEmail(email));
		}

		public Boolean VerificarSeExisteUsuarioPorEmailESenha(string email, string password)
		{
			return RepositorioDeUsuario.VerificarSeExisteUsuarioPorEmailESenha(email, password);
		}

		public bool VerificarSeUsuarioComMesmoEmailJaExiste(Usuario usuario)
		{
			return RepositorioDeUsuario.VerificarSeUsuarioComMesmoEmailJaExiste(usuario);
		}

		public Boolean InserirUsuario(Usuario usuario)
		{
			return RepositorioDeUsuario.InserirUsuario(usuario);	
		}

		public int BuscarQuantidadeRegistros()
		{
			return RepositorioDeUsuario.BuscarQuantidadeRegistros();
		}

		public void RemoverUsuario(Usuario usuario)
		{
			RepositorioDeUsuario.RemoverUsuario(usuario);
		}

		public Boolean RemoverUsuario(int id)
		{
			return RepositorioDeUsuario.RemoverUsuario(id);
		}

		public Boolean AtualizarUsuario(Usuario usuario)
		{
			return RepositorioDeUsuario.AtualizarUsuario(usuario);
		}
	}
}
