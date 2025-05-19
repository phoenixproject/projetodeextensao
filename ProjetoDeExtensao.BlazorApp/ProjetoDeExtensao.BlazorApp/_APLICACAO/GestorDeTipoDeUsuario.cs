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
	public class GestorDeTipoDeUsuario
	{
		public ProjetodeextensaoContext _projetoextensao { get; set; }
		public RepositorioDeTipoDeUsuario RepositorioDeTipoDeUsuario { get; set; }

		public GestorDeTipoDeUsuario(ProjetodeextensaoContext projetoextensao)
		{
			this.RepositorioDeTipoDeUsuario = new RepositorioDeTipoDeUsuario(projetoextensao);
		}

		public TipoUsuario ObterTipoDeUsuarioPorId(int id)
		{
			return RepositorioDeTipoDeUsuario.ObterTipoDeUsuarioPorId(id);
		}

		public List<TipoUsuario> ObterTodosOsTiposDeUsuarios()
		{
			return RepositorioDeTipoDeUsuario.ObterTodosOsTiposDeUsuarios();
		}

		public TipoUsuario ObterTipoDeUsuarioPorDescricao(string descricao)
		{
			return RepositorioDeTipoDeUsuario.ObterTipoDeUsuarioPorDescricao(descricao);
		}

		public bool VerificarSeTipoDeUsuarioComMesmoEmailJaExiste(TipoUsuario tipoUsuario)
		{
			return RepositorioDeTipoDeUsuario.VerificarSeTipoDeUsuarioComMesmaDescricaoJaExiste(tipoUsuario);
		}

		public void InserirTipoDeUsuario(TipoUsuario tipoUsuario)
		{
			RepositorioDeTipoDeUsuario.InserirTipoDeUsuario(tipoUsuario);	
		}

		public int BuscarQuantidadeRegistros()
		{
			return RepositorioDeTipoDeUsuario.BuscarQuantidadeRegistros();
		}

		public void RemoverTipoDeUsuario(TipoUsuario tipoUsuario)
		{
			RepositorioDeTipoDeUsuario.RemoverTipoDeUsuarioUsuario(tipoUsuario);
		}

		public void AtualizarTipoDeUsuario(TipoUsuario tipoUsuario)
		{
			RepositorioDeTipoDeUsuario.AtualizarTipoDeUsuario(tipoUsuario);
		}
	}
}
