using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDeExtensao.BlazorApp._REPOSITORIO
{
	public class RepositorioDeTipoDeUsuario
	{
		public ProjetodeextensaoContext Contexto { get; set; }
		
		public RepositorioDeTipoDeUsuario(ProjetodeextensaoContext context)
		{
			Contexto = context;
		}

		public TipoUsuario ObterTipoDeUsuarioPorId(int id)
		{
			return Contexto.TipoUsuarios.Find(id);
		}

		public List<TipoUsuario> ObterTodosOsTiposDeUsuarios()
		{
			return Contexto.TipoUsuarios.ToList();
		}

		public TipoUsuario ObterTipoDeUsuarioPorDescricao(string descricao)
		{
			return Contexto.TipoUsuarios.Where(l => l.DsTpUsuario.Equals(descricao)).FirstOrDefault();
		}

		public bool VerificarSeTipoDeUsuarioComMesmaDescricaoJaExiste(TipoUsuario tipoUsuario)
		{
			if (Contexto.TipoUsuarios.Where(l => l.DsTpUsuario.Equals(tipoUsuario.DsTpUsuario)).Count() > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void InserirTipoDeUsuario(TipoUsuario tipoUsuario)
		{
			if (!VerificarSeTipoDeUsuarioComMesmaDescricaoJaExiste(tipoUsuario))
			{
				Contexto.TipoUsuarios.Add(tipoUsuario);
				Contexto.SaveChanges();
			}
		}

		public int BuscarQuantidadeRegistros()
		{
			return Contexto.TipoUsuarios.Count();
		}

		public void RemoverTipoDeUsuarioUsuario(TipoUsuario tipoUsuario)
		{
			Contexto.TipoUsuarios.Remove(tipoUsuario);
			Contexto.SaveChanges();
		}

		public void AtualizarTipoDeUsuario(TipoUsuario tipoUsuario)
		{
			if (!VerificarSeTipoDeUsuarioComMesmaDescricaoJaExiste(tipoUsuario))
			{
				Contexto.Entry(tipoUsuario).State = EntityState.Modified;
				Contexto.SaveChanges();
			}
			else
			{
				TipoUsuario tipoUsuario_consulta = ObterTipoDeUsuarioPorDescricao(tipoUsuario.DsTpUsuario);

				if (tipoUsuario_consulta != null)
				{

					Contexto.Entry(tipoUsuario).State = EntityState.Modified;
					Contexto.SaveChanges();
				}
			}
		}
	}
}
