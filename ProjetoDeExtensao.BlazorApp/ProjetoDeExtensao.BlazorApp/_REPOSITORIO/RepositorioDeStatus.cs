using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDeExtensao.BlazorApp._REPOSITORIO
{
	public class RepositorioDeStatus
	{
		public ProjetodeextensaoContext Contexto { get; set; }
		
		public RepositorioDeStatus(ProjetodeextensaoContext context)
		{
			Contexto = context;
		}

		public Status ObterStatusPorId(int id)
		{
			return Contexto.Statuses.Find(id);
		}

		public List<Status> ObterTodosOsStatus()
		{
			return Contexto.Statuses.ToList();
		}

		public Status ObterStatusPorDescricao(string descricao)
		{
			return Contexto.Statuses.Where(l => l.DsStatus.Equals(descricao)).FirstOrDefault();
		}

		public bool VerificarSeStatusComMesmaDescricaoJaExiste(Status status)
		{
			if (Contexto.Statuses.Where(l => l.DsStatus.Equals(status.DsStatus)).Count() > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void InserirStatus(Status status)
		{
			if (!VerificarSeStatusComMesmaDescricaoJaExiste(status))
			{
				Contexto.Statuses.Add(status);
				Contexto.SaveChanges();
			}
		}

		public int BuscarQuantidadeRegistros()
		{
			return Contexto.Statuses.Count();
		}

		public void RemoverStatus(Status status)
		{
			Contexto.Statuses.Remove(status);
			Contexto.SaveChanges();
		}

		public void AtualizarStatus(Status status)
		{
			if (!VerificarSeStatusComMesmaDescricaoJaExiste(status))
			{
				Contexto.Entry(status).State = EntityState.Modified;
				Contexto.SaveChanges();
			}
			else
			{
				Status status_consulta = ObterStatusPorDescricao(status.DsStatus);

				if (status_consulta != null)
				{

					Contexto.Entry(status).State = EntityState.Modified;
					Contexto.SaveChanges();
				}
			}
		}
	}
}
