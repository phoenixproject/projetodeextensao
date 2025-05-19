using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDeExtensao.Shared._MODEL
{
	public class UsuarioDTO
	{
		public int CdUsuario { get; set; }

		public string Email { get; set; } = null!;

		public string Password { get; set; } = null!;

		public string Nome { get; set; } = null!;

		public string Telefone { get; set; } = null!;

		public int TpUsuario { get; set; }

		public int TpServico { get; set; }

		public int CdStatus { get; set; }
	}
}
