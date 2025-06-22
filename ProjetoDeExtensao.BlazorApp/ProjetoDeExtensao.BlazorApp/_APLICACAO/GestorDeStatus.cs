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
	public class GestorDeStatus
	{
		public ProjetodeextensaoContext _projetoextensao { get; set; }
		public RepositorioDeStatus RepositorioDeStatus { get; set; }

		public GestorDeStatus(ProjetodeextensaoContext projetoextensao)
		{
			this.RepositorioDeStatus = new RepositorioDeStatus(projetoextensao);
		}

		public StatusDTO ConverterParaStatusDTO(Status Status)
		{
			return new StatusDTO
			{
				CdStatus = Status.CdStatus,
				DsStatus = Status.DsStatus
			};
		}

		public List<StatusDTO> ConverterListaDeStatusParaFormatoDTO(List<Status> listaDeStatus)
		{
			List<StatusDTO> listaDeStatusDTO = new List<StatusDTO>();
			foreach (var Status in listaDeStatus)
			{
				listaDeStatusDTO.Add(ConverterParaStatusDTO(Status));
			}
			return listaDeStatusDTO;
		}

		public Status ObterStatusPorId(int id)
		{
			return RepositorioDeStatus.ObterStatusPorId(id);
		}

		public List<Status> ObterTodosOsStatus()
		{
			return RepositorioDeStatus.ObterTodosOsStatus();
		}

		public Status ObterStatusPorDescricao(string descricao)
		{
			return RepositorioDeStatus.ObterStatusPorDescricao(descricao);
		}

		public bool VerificarSeStatusComMesmaDescricaoJaExiste(Status status)
		{
			return RepositorioDeStatus.VerificarSeStatusComMesmaDescricaoJaExiste(status);
		}

		public void InserirStatus(Status status)
		{
			RepositorioDeStatus.InserirStatus(status);	
		}

		public int BuscarQuantidadeRegistros()
		{
			return RepositorioDeStatus.BuscarQuantidadeRegistros();
		}

		public void RemoverStatus(Status status)
		{
			RepositorioDeStatus.RemoverStatus(status);
		}

		public void AtualizarStatus(Status status)
		{
			RepositorioDeStatus.AtualizarStatus(status);
		}
	}
}
