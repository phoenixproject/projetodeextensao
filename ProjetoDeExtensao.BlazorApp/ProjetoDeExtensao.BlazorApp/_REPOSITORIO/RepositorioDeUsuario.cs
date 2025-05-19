using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDeExtensao.BlazorApp._REPOSITORIO
{
	public class RepositorioDeUsuario
	{
		public ProjetodeextensaoContext Contexto { get; set; }
		
		public RepositorioDeUsuario(ProjetodeextensaoContext context)
		{
			Contexto = context;
		}

		public Usuario ObterUsuarioPorId(int id)
		{
			return Contexto.Usuarios.Find(id);
		}

		public List<Usuario> ObterTodosOsUsuarios()
		{
			return Contexto.Usuarios.ToList();
		}

		public Usuario ObterUsuarioPorEmail(string email)
		{
			return Contexto.Usuarios.Where(l => l.Email.Equals(email)).FirstOrDefault();
		}

		public bool VerificarSeUsuarioComMesmoEmailJaExiste(Usuario usuario)
		{
			if (Contexto.Usuarios.Where(l => l.Email.Equals(usuario.Email)).Count() > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void InserirUsuario(Usuario usuario)
		{
			if (!VerificarSeUsuarioComMesmoEmailJaExiste(usuario))
			{
				Contexto.Usuarios.Add(usuario);
				Contexto.SaveChanges();
			}
		}

		public int BuscarQuantidadeRegistros()
		{
			return Contexto.Usuarios.Count();
		}

		public void RemoverUsuario(Usuario usuario)
		{
			Contexto.Usuarios.Remove(usuario);
			Contexto.SaveChanges();
		}

		public void AtualizarUsuario(Usuario usuario)
		{
			if (!VerificarSeUsuarioComMesmoEmailJaExiste(usuario))
			{
				Contexto.Entry(usuario).State = EntityState.Modified;
				Contexto.SaveChanges();
			}
			else
			{
				Usuario usuario_consulta = ObterUsuarioPorEmail(usuario.Email);

				if (usuario_consulta != null)
				{

					Contexto.Entry(usuario).State = EntityState.Modified;
					Contexto.SaveChanges();
				}
			}
		}
	}
}
