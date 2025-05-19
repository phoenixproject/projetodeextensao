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
	public class GestorDeTipoDeServico
	{
		public ProjetodeextensaoContext _projetoextensao { get; set; }
		public RepositorioDeTipoDeServico RepositorioDeTipoDeServico { get; set; }

		public GestorDeTipoDeServico(ProjetodeextensaoContext projetoextensao)
		{
			this.RepositorioDeTipoDeServico = new RepositorioDeTipoDeServico(projetoextensao);
		}

		public TipoServico ObterTipoDeServicoPorId(int id)
		{
			return RepositorioDeTipoDeServico.ObterTipoDeServicoPorId(id);
		}

		public List<TipoServico> ObterTodosOsTiposDeServicos()
		{
			return RepositorioDeTipoDeServico.ObterTodosOsTiposDeServicos();
		}

		public TipoServico ObterTipoDeServicoPorDescricao(string descricao)
		{
			return RepositorioDeTipoDeServico.ObterTipoDeServicoPorDescricao(descricao);
		}

		public bool VerificarSeTipoDeServicoComMesmoEmailJaExiste(TipoServico tipoServico)
		{
			return RepositorioDeTipoDeServico.VerificarSeTipoDeServicoComMesmaDescricaoJaExiste(tipoServico);
		}

		public void InserirTipoDeServico(TipoServico tipoServico)
		{
			RepositorioDeTipoDeServico.InserirTipoDeServico(tipoServico);	
		}

		public int BuscarQuantidadeRegistros()
		{
			return RepositorioDeTipoDeServico.BuscarQuantidadeRegistros();
		}

		public void RemoverTipoDeServico(TipoServico tipoServico)
		{
			RepositorioDeTipoDeServico.RemoverTipoDeServicoServico(tipoServico);
		}

		public void AtualizarTipoDeServico(TipoServico tipoServico)
		{
			RepositorioDeTipoDeServico.AtualizarTipoDeServico(tipoServico);
		}
	}
}
