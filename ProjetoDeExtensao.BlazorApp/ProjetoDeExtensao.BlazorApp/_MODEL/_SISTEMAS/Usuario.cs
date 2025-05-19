using System;
using System.Collections.Generic;

namespace ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;

public partial class Usuario
{
    public int CdUsuario { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public int TpUsuario { get; set; }

    public int TpServico { get; set; }

    public int CdStatus { get; set; }

    public virtual Status CdStatusNavigation { get; set; } = null!;

    public virtual TipoServico TpServicoNavigation { get; set; } = null!;

    public virtual TipoUsuario TpUsuarioNavigation { get; set; } = null!;
}
