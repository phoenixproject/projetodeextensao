using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using ProjetoDeExtensao.BlazorApp._REPOSITORIO;
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

		public Usuario ObterUsuarioPorId(int id)
		{
			return RepositorioDeUsuario.ObterUsuarioPorId(id);
		}

		public List<Usuario> ObterTodosOsUsuarios()
		{
			return RepositorioDeUsuario.ObterTodosOsUsuarios();
		}

		public Usuario ObterUsuarioPorEmail(string email)
		{
			return RepositorioDeUsuario.ObterUsuarioPorEmail(email);
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
