using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDeExtensao.BlazorApp._REPOSITORIO
{
	public class RepositorioDeTipoDeServico
	{
		public ProjetodeextensaoContext Contexto { get; set; }
		
		public RepositorioDeTipoDeServico(ProjetodeextensaoContext context)
		{
			Contexto = context;
		}

		public TipoServico ObterTipoDeServicoPorId(int id)
		{
			return Contexto.TipoServicos.Find(id);
		}

		public List<TipoServico> ObterTodosOsTiposDeServicos()
		{
			return Contexto.TipoServicos.ToList();
		}

		public TipoServico ObterTipoDeServicoPorDescricao(string descricao)
		{
			return Contexto.TipoServicos.Where(l => l.DsTpServico.Equals(descricao)).FirstOrDefault();
		}

		public bool VerificarSeTipoDeServicoComMesmaDescricaoJaExiste(TipoServico tipoServico)
		{
			if (Contexto.TipoServicos.Where(l => l.DsTpServico.Equals(tipoServico.DsTpServico)).Count() > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void InserirTipoDeServico(TipoServico tipoServico)
		{
			if (!VerificarSeTipoDeServicoComMesmaDescricaoJaExiste(tipoServico))
			{
				Contexto.TipoServicos.Add(tipoServico);
				Contexto.SaveChanges();
			}
		}

		public int BuscarQuantidadeRegistros()
		{
			return Contexto.TipoServicos.Count();
		}

		public void RemoverTipoDeServicoServico(TipoServico tipoServico)
		{
			Contexto.TipoServicos.Remove(tipoServico);
			Contexto.SaveChanges();
		}

		public void AtualizarTipoDeServico(TipoServico tipoServico)
		{
			if (!VerificarSeTipoDeServicoComMesmaDescricaoJaExiste(tipoServico))
			{
				Contexto.Entry(tipoServico).State = EntityState.Modified;
				Contexto.SaveChanges();
			}
			else
			{
				TipoServico tipoServico_consulta = ObterTipoDeServicoPorDescricao(tipoServico.DsTpServico);

				if (tipoServico_consulta != null)
				{

					Contexto.Entry(tipoServico).State = EntityState.Modified;
					Contexto.SaveChanges();
				}
			}
		}
	}
}
