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

		public Usuario ObterUsuarioPorEmailESenha(string email, string password)
		{
			return Contexto.Usuarios.Where(l => l.Email.Equals(email) && l.Password.Equals(password)).FirstOrDefault();
		}

		public Boolean VerificarSeExisteUsuarioPorEmailESenha(string email, string password)
		{
			if(Contexto.Usuarios.Where(l => l.Email.Equals(email) && l.Password.Equals(password)).Count() > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
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

		public Boolean InserirUsuario(Usuario usuario)
		{
			Boolean resultado = false;

			if (!VerificarSeUsuarioComMesmoEmailJaExiste(usuario))
			{
				Contexto.Usuarios.Add(usuario);
				Contexto.SaveChanges();

				resultado = true;
			}

			return resultado;
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

		public Boolean RemoverUsuario(int id)
		{
			Boolean resultado = false;

			try
			{
				Usuario usuario = ObterUsuarioPorId(id);

				Contexto.Usuarios.Remove(usuario);
				Contexto.SaveChanges();

				resultado = true;
			}
			catch(Exception ex)
			{
				Console.WriteLine($"Erro ao remover usuário: {ex.Message}");
				resultado = false;
			}

			return resultado;
		}

		public Boolean AtualizarUsuario(Usuario usuario)
		{
			Boolean resultado = false;

			if (!VerificarSeUsuarioComMesmoEmailJaExiste(usuario))
			{
				Contexto.Entry(usuario).State = EntityState.Modified;
				Contexto.SaveChanges();

				resultado = true;
			}
			else
			{
				Usuario usuario_consulta = ObterUsuarioPorEmail(usuario.Email);

				if (usuario_consulta != null)
				{

					Contexto.Entry(usuario).State = EntityState.Modified;
					Contexto.SaveChanges();

					resultado = true;
				}
			}

			return resultado;
		}
	}
}
