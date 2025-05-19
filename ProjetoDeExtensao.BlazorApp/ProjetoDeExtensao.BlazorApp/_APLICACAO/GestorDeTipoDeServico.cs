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
	public class GestorDeTipoDeServico
	{
		public ProjetodeextensaoContext _projetoextensao { get; set; }
		public RepositorioDeTipoDeServico RepositorioDeTipoDeServico { get; set; }

		public GestorDeTipoDeServico(ProjetodeextensaoContext projetoextensao)
		{
			this.RepositorioDeTipoDeServico = new RepositorioDeTipoDeServico(projetoextensao);
		}

		public TipoServicoDTO ConverterParaTipoDeServicoDTO(TipoServico tipoServico)
		{
			return new TipoServicoDTO
			{
				TpServico = tipoServico.TpServico,
				DsTpServico = tipoServico.DsTpServico
			};
		}

		public List<TipoServicoDTO> ConverterListaDeTipoDeServicoParaFormatoDTO(List<TipoServico> listaDeTipoDeServico)
		{
			List<TipoServicoDTO> listaDeTipoDeServicoDTO = new List<TipoServicoDTO>();
			foreach (var tipoServico in listaDeTipoDeServico)
			{
				listaDeTipoDeServicoDTO.Add(ConverterParaTipoDeServicoDTO(tipoServico));
			}
			return listaDeTipoDeServicoDTO;
		}

		public TipoServicoDTO ObterTipoDeServicoPorId(int id)
		{
			return ConverterParaTipoDeServicoDTO(RepositorioDeTipoDeServico.ObterTipoDeServicoPorId(id));
		}

		public List<TipoServicoDTO> ObterTodosOsTiposDeServicos()
		{
			return ConverterListaDeTipoDeServicoParaFormatoDTO(RepositorioDeTipoDeServico.ObterTodosOsTiposDeServicos());
		}

		public TipoServicoDTO ObterTipoDeServicoPorDescricao(string descricao)
		{
			return ConverterParaTipoDeServicoDTO(RepositorioDeTipoDeServico.ObterTipoDeServicoPorDescricao(descricao));
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
